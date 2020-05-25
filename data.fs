\ data structures for the editor

\ Single line
struct
   cell% field line-next		\ next line
   cell% field line-prev		\ previous line
   cell% field text			\ link to the contents of the line
end-struct line%

\ The buffer
struct
   cell% field buffer-line		\ line number
   cell% field buffer-column		\ column number
   cell% field buffer-name		\ buffer display name
   cell% field buffer-filename		\ name of the associated file
   cell% field buffer-first-line	\ link to the first line
   char% field buffer-modified		\ modified flag
   char% field buffer-readonly		\ buffer is read-only flag
   char% field buffer-draft		\ buffer is a draft flag
end-struct buffer%
