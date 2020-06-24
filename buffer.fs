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


\ data structures for the buffer

\ ****s* buffer/buffer-line%
\ FUNCTION
\   Represents a node of a doubly linked list of the buffer.
\   * prev: pointer to the previous line
\   * next: pointer to the next line
\   * text: pointer to the string
\ ******
struct
   cell% field buffer-line-prev		\ previous line
   cell% field buffer-line-next		\ next line
   cell% field biffer-line-text		\ link to the contents of the line
end-struct buffer-line%

\ ****s* buffer/buffer%
\ FUNCTION
\   Represents the buffer with its properties and content.
\   * line: line number
\   * name: buffer name
\   * filename: name of the associated file
\   * first-line: link to the first line
\   * modified: modified flag
\   * buffer-readonly: buffer is read-only flag
\   * draft: buffer is a draft flag
\ ******
struct
   cell% field buffer-line
   cell% field buffer-name
   cell% field buffer-filename
   buffer-line% field buffer-first-line
   char% field buffer-modified
   char% field buffer-readonly
   char% field buffer-draft
end-struct buffer%

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
