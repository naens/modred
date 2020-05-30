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
variable line-ptr \ pointer to current line
variable line-num \ height of text

\ basic functions
: go-up ;
: go-down ;
: go-end-of-line ;
: go-begin-of-line ;
: insert-character ;
: delete-character ;
: delete-line ;
: split-line ;
: merge-lines ;

\ helper functions
: string-move  ( string-address address-destination -- )
   over c@ 1+ move ;
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
      line-ptr @ line-text @ edit-line-buffer string-move
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
      line-ptr @ line-text @ edit-line-buffer string-move
      line-pos @ current-line-length min line-pos !
   else
      drop
   then ;
: cmd-go-end-of-line ;
: cmd-go-begin-of-line ;

\ editing commands
: update-line  ( -- )
   form . line-num -
   0 0 at-xy \ TODO: fix
   edit-line-buffer count type ;
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
      -1 line-pos 1+
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
: cmd-split-line-left ;
: cmd-split-line-right ;

: update-text ( text-buffer% -- )
   0 form drop line-num @ - 1- at-xy
   dup text-buffer-first-line @
   begin
      dup 0<> while
      dup line-text @ count cr type ." |||"
      line-next @
   repeat
   drop ;

\ intialize data
: do-edit-init  ( text-buffer% -- text-buffer% )

   0 line-num !
   dup text-buffer-first-line @
   begin
      dup 0<> while

      \ increase line counter
      line-num @ 1+ line-num !

      \ copy current line into the buffer
      over text-buffer-line @ line-num @ = if
         dup line-text @ edit-line-buffer string-move
         dup line-ptr !
      then

      \ type line
      dup line-text @ count cr type
      line-next @
   repeat
   drop

   \ check that current line is in buffer
\   cr ." ---> " edit-line-buffer count type

   \ initialize line position
   0 line-pos ! ;

\ edit: process keyboard events until ^Z is pressed
: do-edit  ( text-buffer% -- )
   do-edit-init
   begin
      dup text-buffer-line @ ( buffer current-line-number )
      form drop + line-pos @ swap line-num @ - 1- at-xy
      ekey dup ctrl-q <> while
      dup
      case
         ctrl-s of cmd-go-left drop endof
         ctrl-e of over cmd-go-up drop endof
         ctrl-d of cmd-go-right drop endof
         ctrl-x of over cmd-go-down drop endof
         cmd-insert-character
      endcase
   repeat drop ;
