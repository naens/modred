struct Parser<'a> {
    text: &'a str,
    index: usize
}

enum CommandId {
    V,A,W,E,					// 4.3
    B,C,L,Number,T,D,K,I,S,			// 4.4
    P,LineNumber, ThroughLineNumber,		// 4.6.1
    F,N,J,M,Z,					// 4.6.2
    X,R,					// 4.6.3
    H,O,Q					// 4.6.4
}

struct Command<'a> {
    prefix: &'a str,
    command: CommandId,
    tail: &'a str
}

impl<'a> Parser<'a> {
    fn new(text: &str) -> Parser {
        Parser {
            text: text,
            index: 0
        }
    }
}

impl<'a> Iterator for Parser<'a> {
    type Item = Command<'a>;

    fn next(&mut self) -> Option<Command<'a>> {
        None
    }
}
