\ ****h* Modred/Line
\ FUNCTION
\   Manage a single line of text.  Allocate multiples
\   of line-chunk-size.  Prevents from resizing after
\   each insertion or deletion.  When making the line
\   smaller, its capacity is not changed.  The capacity
\   is changed only when more space is required than
\   the current capacity.
\   A line is represented by a structure containing
\   the capacity and the pointer to the string.
\ ******

16 constant line-chunk-size

\ ****s* Line/line%
\ FUNCTION
\   Represents the line in the buffer.  Also contains
\   the capacity in order to make reallocations less
\   frequent.
\ ******
struct
    cell% field line-text
    cell% field line-length
    cell% field line-capacity
end-struct line%


\ initialize new empty line
: line-new ( -- line% )
   line% %size allocate throw  \ TODO exception???
   line-chunk-size allocate throw over line-text ! \ TODO exception???
   0 over line-length !
   line-chunk-size over line-capacity ! ;


\ TODO: insert character at
: line-insert-character ( line% c u -- )
   ;


\ TODO: delete character at
: line-delete-character ( line% u -- )
   ;

\ delete line
: line-delete ( line% -- )
   dup line-text @ free drop
   free drop ;

\ ****f* Line/line-ensure-capacity
\ FUNCTION
\   Ensure that the line has at least n capacity.
\   Does nothing if there is enough capacity.
\   Otherwise calculates the required capacity and
\   allocates in the text.
\ PSEUDOCODE
\   if u < line.capacity then
\      let c be line-chunk-size
\      new_capacity := ((u+c-1)/c)*c
\      line.text := resize(new_capacity)
\      line.capacity := new_capacity
\   end if
\ ******
: line-ensure-capacity ( line% u -- )
   over line-capacity @   ( line% u capacity )
   over > if              ( line% u )
      line-chunk-size     ( line% u c )
      swap over 1- +      ( line% c u+c-1 )
      over / *            ( line% new_capacity )
      2dup swap           ( line% new_capacity new_capacity line% )
      line-capacity !     ( line% new_capacity )
      over line-text @    ( line% new_capacity line-text )
      resize throw        ( line% new_text )
      swap line-text !    ( )
   else                   ( line% u )
      2drop
   then ;
   

\ ****f* Line/line-set-string
\ FUNCTION
\   Set the string to the line.  Reallocates if necessary.
\   If the string does not fit into the capacity, allocates
\   a multiple of the minimum capacity number of bytes.
\ PSEUDOCODE
\   line_ensure_capacity(line%, u)
\   copy string <addr,u> to line.text
\ ******
: line-set-string  ( line% addr u -- )
   rot swap 2dup swap   ( addr u line% line% u )
   line-ensure-capacity ( addr u line% )
   line-text @ 1+       ( addr u line-text+1 )
   rot 1+               ( u line-text+1 addr+1 )
   swap rot             ( addr+1 line.text+1 u )
   move ;

: line-get-string ( line% -- string )
   line-text @ ;
