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

\ TODO: initialize new empty line
: line-new ( -- line% )
   ;


\ TODO: insert character at
: line-insert-character ( line% c u -- )
   ;

\ TODO: delete character at
: line-delete-character ( line% u -- )
   ;

\ TODO: delete line
: line-delete ( line% -- )
   ;

\ TODO: set string
: line-set-string ( line% addr u -- )
   ;
