extern crate rustyline;

use std::env;
use std::ffi::OsStr;
use std::fs::metadata;
use std::path::Path;

use rustyline::error::ReadlineError;
use rustyline::Editor;

mod control; 
mod textbuf;
use control::Control;

fn print_usage(pname: &str) {
    println!("Usage:");
    println!("{} <file>", pname);
    println!("{} <file-in> <file-out>", pname);
    println!("{} <file-in> <path-out>", pname);
}

fn main() {
    let args: Vec<String> = env::args().collect();

    if args.len() < 2 || args.len() > 3 {
        print_usage(&args[0]);
        return;
    }

    let file_in = &args[1];
    let mut file_out = String::from("");
    if args.len() == 2 {
        file_out.push_str(&file_in);
    } else {
        let in_path =  Path::new(&args[1]);
        let in_name = OsStr::to_str(in_path.file_name().unwrap()).unwrap();
        if !in_path.exists() {
            println!("File {} does not exist", in_name);
            return;
        }
        let out_path =  Path::new(&args[2]);
        if out_path.is_dir() {
            file_out.push_str(&args[2]);
            file_out.push_str("/");
            file_out.push_str(in_name);
        } else {
            file_out.push_str(&args[2]);
        }
    }
    println!("file_in={}, file_out={}", file_in, file_out);
   
    let mut r1 = Editor::<()>::new();
    let control = Control::new();
    loop {
        let prompt = control.get_prompt();
        let readline = r1.readline(prompt);
        match readline {
            Ok(line) => {
                control.execute(&line);
            },
            Err(ReadlineError::Interrupted) => {
                println!("INT");
                break;
            }
            Err(ReadlineError::Eof) => {
                println!("EOF");
                break;
            },
            Err(err) => {
                println!("Error: {:?}", err);
                break;
            }
        }
    }
}
