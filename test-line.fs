\ This module is for testing that the line module works


: test1 \ the new line should be empty
   line-new            ( line% )
   dup line-get-string ( line% string )
   swap line-delete    ( string )
   count drop 0= ;

: test2 \ new line with a string inside
   line-new                ( line% )
   dup s" abcd" set-string ( line% )
   dup line-get-string     ( line% string )
   swap line-delete        ( string )
   count .s" abcd" str= ;
   