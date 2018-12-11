struct Parser<'a> {
    text: &'a str,
    index: usize
}

enum CommandId {
}

struct Command<'a> {
    prefix: &'a str,
    command: CommandId,
    tail: &'a str
}

impl<'a> Iterator for Parser<'a> {
    type Item = Command<'a>;

    fn next(&mut self) -> Option<Command> {
    }
}
