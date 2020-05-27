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


\ editor variables
create edit-line-buffer 256 allot
variable line-pos
variable line-ptr

\ basic functions
: go-left ;
: go-up ;
: go-right ;
: go-down ;
: go-end-of-line ;
: go-begin-of-line ;
: insert-character ;
: delete-character ;
: delete-line ;
: split-line ;
: merge-lines ;

\ editing commands
: cmd-go-left ;
: cmd-go-up ;
: cmd-go-right ;
: cmd-go-down ;
: cmd-go-end-of-line ;
: cmd-go-begin-of-line ;
: cmd-insert-character ." insert" . ;
: cmd-delete-left ;
: cmd-delete-right ;
: cmd-split-line-left ;
: cmd-split-line-right ;

\ intialize data
: do-edit-init  ( text-buffer% -- text-buffer% )
   \ TODO: initialize line pointer
   \ TODO: copy current line into the buffer

   \ initialize line position
   0 line-pos ! ;

\ edit: process keyboard events until ^Z is pressed
: do-edit  ( text-buffer% -- )
   do-edit-init
   begin
      ekey dup ctrl-q <> while
      dup
      case
         ctrl-s of cmd-go-left drop endof
         ctrl-e of cmd-go-up drop endof
         ctrl-d of cmd-go-right drop endof
         ctrl-x of cmd-go-down drop endof
         cmd-insert-character
      endcase
   repeat drop ;
