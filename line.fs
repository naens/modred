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

\ ****f* Line/line-set-string
\ FUNCTION
\   Set the string to the line.  Reallocates if necessary.
\   If the string does not fit into the capacity, allocates
\   a multiple of the minimum capacity number of bytes.
\ PSEUDOCODE
\   let c be minimum capacity ("line-chunk-size")
\   If u < capacity then
\      line.text := reallocate(((u+(c-1)) div c) * c)
\   end if
\   copy string <addr-u> to line.text
: line-set-string       ( line% addr u -- )
   dup 2over            ( line% addr u u line% addr )
   -rot                 ( line% addr u addr u line% )
   line-capacity @ over ( line% addr u capacity u )
   > if                 ( line% addr u )
      line-chunk-size   ( line% addr u c )
      2dup              ( line% addr u c u c )
      dup 1- rot +      ( line% addr u c c u+c-1 )
      swap / *          ( line% addr u sz )
      resize throw      ( line% addr u new )
      3 pick line-text !
   then                 ( line% addr u )
   swap 1+              ( line% u addr+1 )
   rot line-text @ 1+   ( u addr+1 line.text+1 )
   -rot                 ( addr+1 line.text+1 u )
   move ;

: line-get-string ( line% -- string )
   line-text @ ;
