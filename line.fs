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

constant line-chunk-size 16


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
  line-chunk-size swap line-capacity !;


\ TODO: insert character at
: line-insert-character ( line% c u -- )
   ;


\ TODO: delete character at
: line-delete-character ( line% u -- )
   ;

\ delete line
: line-delete ( line% -- )
   dup line-text @ free
   free ;

\ TODO: set string
: line-set-string ( line% addr u -- )
   ;
