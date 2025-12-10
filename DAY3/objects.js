//Objects
//Objects store in key-value pairs

let student = {
    name : "Rahul",
    age : 22,
    course : "Javascript"
}

//Accessing properties
// student.name // Dot notation (STATIC access, not dynamic)
//student["age"] //Bracket notation supports dynamic access
// console.log(student[key]); //javascript

let key = "course";
console.log(student[key]); // prints "Javascript

//adding properties
student.city = "delhi";
delete student.age;

console.log(typeof(student))

//Converting objects to json string

let jsonString = JSON.stringify(student);

//Convert JSON String to Object
let obj = JSON.parse(jsonString);

console.log(typeof(obj))

console.log(student === obj);
