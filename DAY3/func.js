function run(n){
    return n * 2;
}

let n = run(25)
let a = [1,2,3,"apple","banana",n];
a.push(23)
a.pop()

console.log(a)