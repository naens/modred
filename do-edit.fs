\ do-edit is a word that receives a string and returns a linked list
\ of lines of text.  Each linked list is a structure of type line% .
\ Editing one line can create multiple lines by splitting lines.
\ A line can be split with ^M and with ^N

\ editor constants
17 constant ctrl-q
26 constant ctrl-z
19 constant ctrl-s
5 constant ctrl-e
4 constant ctrl-d
24 constant ctrl-x
14 constant ctrl-n
13 constant ctrl-m
7 constant ctrl-g
8 constant ctrl-h
10 constant ctrl-j
25 constant ctrl-y
20 constant line-max-length


\ editor variables
create edit-line-buffer 256 allot
variable line-pos \ cursor column in current line
variable first-line \ cursor to the first line
variable line-ptr \ pointer to current line
variable line-num \ current line number
variable text-height \ height of text

\ basic functions
: go-up ;
: go-down ;
: go-end-of-line ;
: go-begin-of-line ;
: insert-character ;
: delete-character
   \ TODO: delete character at pos and reduce size
   edit-line-buffer 1+ line-pos @ +  ( to )
   dup 1+ swap                       ( from to )
   edit-line-buffer c@ line-pos @ -  ( from to len )
   move
   -1 edit-line-buffer c@ + edit-line-buffer c! \ decrease length
   ;
: delete-line ;

\ Synchronize structures with the buffer,
\ while splitting the line at cursor.
\ the buffer and the variables are not updated
: split-line
   \ TODO: allocate space for text in the current line structure
   \ TODO: move the first half of the buffer to current line structure
   \ TODO: create structure for the next line
   \ TODO: allocate space for text in the next line structure
   \ TODO: move the second half of the buffer to the next line structure
   ;
: merge-lines ;

\ helper functions
: current-line-length  ( -- n )
   edit-line-buffer c@ ;
: first-line?  ( -- flag )
   line-ptr @ line-prev @ 0<> ;
: last-line?  ( -- flag )
   line-ptr @ line-next @ 0<> ;

\ navigation commands
: cmd-go-left
   line-pos @ dup 0 > if 1- line-pos ! else drop then ;
: cmd-go-up  ( buffer -- )
   line-ptr @ line-prev @ 0<> if
      -1 swap text-buffer-line +!
      line-ptr @ line-prev @ line-ptr !
\ TODO      line-ptr @ line-text @ edit-line-buffer string-move
      line-pos @ current-line-length min line-pos !
   else
      drop
   then ;
: cmd-go-right
   line-pos @ dup current-line-length < if 1+ line-pos ! else drop then ;
: cmd-go-down  ( buffer -- )
   line-ptr @ line-next @ 0<> if
      1 swap text-buffer-line +!
      line-ptr @ line-next @ line-ptr !
\ TODO      line-ptr @ line-text @ edit-line-buffer string-move
      line-pos @ current-line-length min line-pos !
   else
      drop
   then ;
: cmd-go-end-of-line ;
: cmd-go-begin-of-line ;

\ editing commands
: update-line  ( -- )
   0 form drop text-height @ - line-num @ + at-xy
   edit-line-buffer count type
   form swap drop edit-line-buffer c@ - 0 do space loop ;
: cmd-insert-character
   line-pos @ line-max-length < if
      edit-line-buffer line-pos @ + 1+ dup 1+
      current-line-length line-pos @ - move
      edit-line-buffer 1+ line-pos @ + c!
      current-line-length 1+ edit-line-buffer c!
      line-pos @ 1+ line-pos !
      update-line
   then ;
: cmd-delete-left
   line-pos @ 0> if
      -1 line-pos +!
      delete-character
      update-line
   else
      line-pos @ 0= first-line? and if
         \ merge lines to the left
         update-line
      then
   then ;
: cmd-delete-right 
   line-pos @ current-line-length < if
      delete-character
      update-line
   else
      line-pos @ current-line-length = last-line? and if
         \ merge lines to the right
         update-line
      then
   then ;
: cmd-split-line-left
   split-line
   \ TODO: shift second half of the buffer to the beginning
   \ TODO: set lineptr to next
   \ TODO: set cursor position to 0
   \ TODO: increment current line number
   ;
: cmd-split-line-right
   split-line
   \ TODO: truncate buffer to cursor position
   ;

: update-text ( text-buffer% -- )
   0 form drop text-height @ - 1- at-xy
   dup text-buffer-first-line @
   begin
      dup 0<> while
      dup line-text @ count cr type ." |||"
      line-next @
   repeat
   drop ;

\ intialize data
: do-edit-init  ( string length -- text-buffer% )

   1 text-height !

  \ copy current line into the temporary buffer
   ( s l )
   edit-line-buffer 2dup ! ( s l b )
   1+ swap move

   \ create the line structure
   line% %size allocate drop line-ptr !
   line-ptr @ line-prev 0 swap !
   line-ptr @ line-next 0 swap !
   line-ptr @ line-text 0 swap !  \ 0: no text yet
   line-ptr @ first-line !

   \ type line
   0 form drop 1- at-xy
   edit-line-buffer count type

   \ initialize line position
   1 line-num ! \ count lines from 1 and characters from 0
   0 line-pos ! ;

: ascii-printable? ( c -- ? )
   dup bl >=
   swap [char] ~ <=
   and ;

\ edit: process keyboard events until ^Z is pressed
: do-edit  ( string length -- line% )
   do-edit-init
   begin
\      dup text-buffer-line @ ( buffer current-line-number )
      line-num form drop + line-pos @ swap line-num @ - 1- at-xy
      ekey dup ctrl-z <> while
      dup
      case
         ctrl-s of cmd-go-left drop endof
         ctrl-e of over cmd-go-up drop endof
         ctrl-d of cmd-go-right drop endof
         ctrl-x of over cmd-go-down drop endof
         ctrl-h of cmd-delete-left drop endof
         ctrl-g of cmd-delete-right drop endof
         ctrl-m of cmd-split-right drop endof
         ctrl-n of cmd-split-left drop endof
         dup ascii-printable? if
            cmd-insert-character
         else
            drop
         then
      endcase
   repeat drop ;
