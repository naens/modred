\ editor constants
17 constant ctrl-q
26 constant ctrl-z
19 constant ctrl-s
5 constant ctrl-e
4 constant ctrl-d
24 constant ctrl-x


\ editor variables
create edit-line 256 allot
variable line-pos

\ edit: process keyboard events until ^Z is pressed
: do-edit ( buffer% -- )
   begin
      ekey dup ctrl-q <> while
      dup
      case
         ctrl-s of ." left " drop endof
         ctrl-e of ." up " drop endof
         ctrl-d of ." right " drop endof
         ctrl-x of ." down " drop endof
         .
      endcase
   repeat drop ;
