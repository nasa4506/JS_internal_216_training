let marks=[80,90,70,85,95];

let avg=marks.reduce((acc,val)=>acc+val,0)/marks.length;

console.log(avg);


let numbers=[1,2,3,4,5,6,7,8,9];

let arr1=numbers.filter(n=>n%2==0);

console.log(arr1);


let cart={
    item:"Laptop",
    price:45000,
    quantity:2
};

let total_bill = cart.price * cart.quantity;

console.log(total_bill);


let movies = ["Inception", "Interstellar", "The Dark Knight", "Titanic", "Avatar"];


movies.push("Joker");

console.log(movies);

movies.pop();

for(val of movies)
{
    console.log(val);
}


let user = {
    name:"Aman",
    age:21,
    course:"JS"
};
console.log(user);
let jsondata=JSON.stringify(user)

console.log(jsondata);
let obj=JSON.parse(jsondata);

console.log(obj);


let arr2=[10,40,25,80,15];

let max=arr2[0];

for(let i=0;i<arr2.length;i++)
{
    if(arr2[i]>max)
    {
        max=arr2[i];
    }
}
console.log(max);



let names=["ram","shyam","mohan"];

let new_names=names.map(n=>n.toUpperCase());

console.log(new_names)