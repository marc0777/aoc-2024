use std::fs;
use regex::Regex;
extern crate regex;

fn p1(contents: &str) -> i32 {
    let re = Regex::new(r"mul\((\d*),(\d*)\)").unwrap();

    let mut sum = 0;
    for (_, [a, b]) in re.captures_iter(&contents).map(|c| c.extract()) {
        let ai: i32 = a.parse().unwrap();
        let bi: i32 = b.parse().unwrap();

        sum += ai * bi;
    }

    return sum;
}

fn p2(contents: &str) -> i32 {
    let re = Regex::new(r"don't\(\)((.|\n)*?)do\(\)").unwrap();
    let cleaned = re.replace_all(contents, "");
    return p1(&cleaned);
}


fn main() {
    let file_path = "input.txt";
    println!("In file {file_path}");

    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let res1 = p1(&contents);
    println!("Result is {res1}");
    let res2 = p2(&contents);
    println!("Result is {res2}");

}