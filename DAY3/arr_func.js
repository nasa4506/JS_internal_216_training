
console.log("ARRAY METHODS:     ")
let a = ['apple','orange','banana','kiwi','avocado']
a.push('papaya')
a.pop()
a.unshift('kiwi')
console.log(a.includes('mango'))
console.log(a.indexOf('banana'))

console.log("/////////First LOOP//////////")
for(let i = 0; i< a.length; i++){
    console.log(a[i]);
}

//using for each
console.log("/////////SECOND LOOP//////////")
for(let i of a){
    console.log(i);
}
console.log("/////////Higher order array methods////////")
// Map ()  => create a new array by applying operation on each elements
// filter() => Returns new array of elements that satisfy a condition
//reduce() => The reduce method iterate through an array to provide a single resultant value
// //let number - [1,2,3];

//map example
let number = [1,2,3]
let doubled = number.map(n=>n*2)
console.log(doubled)

//filter example
let nums = [10,25,30,40];
let result = nums.filter(n=> n> 20);
console.log(result)

//reduce example syntax => 
//Array.reduce((accumulator.currentValue)=> {...} initialValue)
// accumulator(acc) Stores the result as a the loop continues
//currentValue(val) Current element of array being proceesed
//initialValue Starting value for the accumulator

//example
console.log("/////////reduce function example/////////")
let num = [2,4,6,8]
let total = num.reduce((acc,val) => acc+val,0)
console.log(total);