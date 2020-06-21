\ ****h* modred/buffer
\ FUNCTION
\    The Buffer module contains words for the management of
\    Buffer objects.  That is the creation, destruction, data,
\    and cursor position.
\    The words are meant to be re-entrant.  It contains words
\    for creation and destruction, but no data is stored in the
\    module.
\    The Buffer contains the whole text and the line and the
\    column of the cursor.
\    The Buffer is responsible for managing the files: it can
\    create a file, open a file, and read a file.
\ ******


\ TODO: creation and initialization words

\ create new
\ set file name
\ read file

\ TODO: Querying
\ get number of lines
\ get line by number
\ ?iterator/iterate

\ TODO: insertion words
\ append line
\ insert line
\ insert character

\ TODO: deletion words
\ delete line
\ delete character
\ merge lines

\ TODO: navigation words
\ next line
\ previous line
\ next character
\ previous character
\ split line
