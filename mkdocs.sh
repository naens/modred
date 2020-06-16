#!/bin/sh
function robodoc_html()
{
    robodoc --src . --doc ./docs --html --multidoc \
        --sections --tell --toc --index --css robodoc.css

    for f in docs/*.html
    do
        sed -i -e 's/toc_index.html/index.html/' "$f"
    done

    mv docs/toc_index.html docs/index.html
}

function robodoc_pdf()
{
    mkdir -p tex
    robodoc --src . --doc tex/ksi --latex --singledoc \
        --sections --tell --toc --index
    cd tex
    pdflatex ksi.tex
    if [ -f "ksi.pdf" ]
    then
        mv ksi.pdf ..
    fi
}

for var in "$@"
do
    if [ "$var" = "html" ]
    then
        robodoc_html
    elif [ "$var" = "pdf" ]
    then
        robodoc_pdf
    fi
done