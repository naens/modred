#[derive(Debug, PartialEq)]
struct Parser<'a> {
    text: &'a str,
    index: usize
}

#[derive(Debug, PartialEq)]
enum CommandId {

    // 4.3
    VerifyLineNumbers, Append, Write, Exit,

    // 4.4
    B, Character, L, Number, T, D, K, I, S,

    // 4.6.1
    P, LineNumber, ThroughLineNumber,

    // 4.6.2
    F, N, J, M, Z,

    // 4.6.3
    X, R,

    // 4.6.4
    HeadOfFile, Original, Quit
}

#[derive(Debug, PartialEq)]
enum CmdNum {
    Minus,
    Hash,
    Number(i32)
}

#[derive(Debug, PartialEq)]
struct Command<'a> {
    prefix: Option<CmdNum>,
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

    fn parse_number(&mut self) -> i32 {
        let mut result: i32 = 0;
        let mut count = 0;
        println!("number? {}", &self.text[self.index..]);
        loop {
            let c = self.text[self.index..].chars().next().unwrap();
            if c >= '0' && c <= '9' {
                result *= 10;
                result += c.to_digit(10).unwrap() as i32;
                count += 1;
                self.index += 1;
            } else {
                break;
            }
        }
        result
    }

    fn parse_prefix(&mut self) -> Option<CmdNum> {
        match self.text[self.index..].chars().next().unwrap() {

            // # => hash
            '#' => {
                self.index += 1;
                Some(CmdNum::Hash)
            }

            '-' => {
                self.index += 1;
                match self.text[self.index..].chars().next().unwrap() {

                    // -number => negative number
                    '0' ... '9' => Some(CmdNum::Number(-self.parse_number())),

                    // - => minus
                    _ => Some(CmdNum::Minus)
                }
            }

            // number => positive number
            '0' ... '9' => Some(CmdNum::Number(self.parse_number())),

            // else => none
            _ => None
        }
    }

    // 4.3
    fn parse_verify_line_numbers(&mut self) -> (Option<CommandId>, &'a str) {
        self.index += 1;
        (Some(CommandId::VerifyLineNumbers), "")
    }

    fn parse_append(&mut self) -> (Option<CommandId>, &'a str) {
        self.index += 1;
        (Some(CommandId::Append), "")
    }

    fn parse_exit(&mut self) -> (Option<CommandId>, &'a str) {
        if &self.text[self.index..] == "E" {
            (Some(CommandId::Exit), "")
        } else {
            (None, "")
        }
    }

    // 4.4
    fn parse_character(&mut self) -> (Option<CommandId>, &'a str) {
        self.index += 1;
        (Some(CommandId::Character), "")
    }

    // 4.6.4
    fn parse_head_of_file(&mut self) -> (Option<CommandId>, &'a str) {
        if &self.text[self.index..] == "H" {
            (Some(CommandId::HeadOfFile), "")
        } else {
            (None, "")
        }
    }

    fn parse_original(&mut self) -> (Option<CommandId>, &'a str) {
        if &self.text[self.index..] == "O" {
            (Some(CommandId::Original), "")
        } else {
            (None, "")
        }
    }

    fn parse_quit(&mut self) -> (Option<CommandId>, &'a str) {
        if &self.text[self.index..] == "Q" {
            (Some(CommandId::Quit), "")
        } else {
            (None, "")
        }
    }


    fn parse_command(&mut self) -> (Option<CommandId>, &'a str) {
        match self.text[self.index..].chars().next().unwrap() {

            // 4.3
            'V' => self.parse_verify_line_numbers(),
            'A' => self.parse_append(),
            'E' => self.parse_exit(),

            // 4.4
            'C' => self.parse_character(),

            // 4.6.6
            'H' => self.parse_head_of_file(),
            'O' => self.parse_original(),
            'Q' => self.parse_quit(),

            _ => (None, "")
        }
    }
}

impl<'a> Iterator for Parser<'a> {
    type Item = Command<'a>;

    fn next(&mut self) -> Option<Command<'a>> {
        if self.text.len() == self.index {
            return None;
        }
        let prefix = self.parse_prefix();
        let (command, tail) = self.parse_command();
        match command {
            Some(cmd) => Some(Command {
                prefix: prefix,
                command: cmd,
                tail: tail
            }),
            _ => None
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    // 4.3 tests
    #[test]
    fn test_verify_line_numbers() {
        let mut parser = Parser::new("V-V0V");
        assert_eq!(parser.next(),
            Some(Command {
                prefix: None,
                command: CommandId::VerifyLineNumbers,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Minus),
                command:CommandId::VerifyLineNumbers,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Number(0)),
                command: CommandId::VerifyLineNumbers,
                tail: "" }));
    }

    #[test]
    fn test_append() {
        let mut parser = Parser::new("A0A#A12A");
        assert_eq!(parser.next(),
            Some(Command {
                prefix: None,
                command: CommandId::Append,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Number(0)),
                command: CommandId::Append,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Hash),
                command:CommandId::Append,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Number(12)),
                command: CommandId::Append,
                tail: "" }));
        assert_eq!(parser.next(), None);
    }

    #[test]
    fn test_exit() {
        let mut parser = Parser::new("E");
        assert_eq!(parser.next(),
            Some(Command {
                prefix: None,
                command: CommandId::Exit,
                tail: "" }));
    }

    // 4.4 tests
    #[test]
    fn test_character() {
        let mut parser = Parser::new("A30C-7C");
        assert_eq!(parser.next(),
            Some(Command {
                prefix: None,
                command: CommandId::Append,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Number(30)),
                command: CommandId::Character,
                tail: "" }));
        assert_eq!(parser.next(),
            Some(Command {
                prefix: Some(CmdNum::Number(-7)),
                command:CommandId::Character,
                tail: "" }));
        assert_eq!(parser.next(), None);
    }

}
