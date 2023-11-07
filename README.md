# Swlang

A swahili based programming language

## Introduction

Swlang is just another programming language that uses swahili keywords and actions.
This is just a fun side project that will help anyone understand the inner workings of compilers and interpreters

## How it Works!

```text
// This is my first swlang program
sema("Salamu, Dunia!");
```
Translation

- sema -> say
- Salamu -> Hello
- Dunia -> World

### Features

Swlang is a high-level dynamically typed programming language

1. **Dynamic Typing** - Variables can store values of any type, and a single
   variable can even store values of different types at different times.
2. **Automatic Memory Management** - Swlang uses a Garbage Collector (GC) for memory management. Therefore there is no need to stress yourself with memory leaks, the GC will take care of that for you

### Data Types

#### 1. Boolean
Swlang uses ```ukweli``` and ```uwongo``` which are Swahili words that translate to ```true``` and ```false``` respectively in English.

```text
ukweli; //true
uwongo; //false
```

#### 2. Numbers

Swlang only features double-precision floating point numbers since they can represent a wide range of integers that covers a lot of teritory while keeping things simple.

```text
12; //integer
12.59; //decimal
```

#### 3. Strings

String literals are enclosed in double quotes.

```text
"Salamu, Dunia!"; // string literal
"J"; // character
```

#### 4. Null

```hakuna``` represents "null"/"no value".

### Expressions

There are various expressions supported in Swlang

#### 1. Arithmetic

Swlang features the basic arithmetic expressions from other popular language.

```text
1 + 2; //addition - kujumuisha
a - b; //subtraction - kuondoa
c * d; //multiplication - kuzidisha
4 / 2; //division - kugawa
```

#### 2. Comparison and Equality

These operations always return a boolean

```text
a < b; //less than
b > a; //greater than
a <= b; //less than or equal to
b >= a; //grater than or equal to
a == b; // equal to
```

#### 3. Logical Operators

```text
!ukweli; //equates to uwongo. !true -> false
!uwongo; //equates to ukweli. !false -> true
```

There is also control flow expressions.

```text
ukweli na uwongo; //true and false -> false
ukweli na ukweli; //true and true -> true
ukweli ama uwongo; //true or false -> true
uwongo ama ukweli; //false or true -> true
```

#### 4. Precedence and Grouping

```text
chombo jumla = (1 + 2) * 4;
```

#### 5. Statements

In Swlang statements are followed by a semi-colon to indicate the end of the statement;

```text
sema("Salamu, Dunia!");
```

You can also use curly braces ```{}``` to wrap your statements/expressions in a code block

```text
{
   chombo c = a + b;
   sema("Salamu, Dunia");
}
```

#### 6. Variables

Variables are declared using the ```chombo``` keyword. Chombo is a swahili word for container and I usually think of variables as containers which store different items/values. The ```chombo``` keyword is equivalent to ```var```/```let``` in other popular languages.

```text
chombo a = 1;
sema(a);                //"a"
chombo jina = "Juma";
sema(jina);             //"Juma"
```

#### 7. Control Flow

Control flow mainly involves if statements and loops.

```text
ikiwa(a > b) {
    sema("ndio");
} isipo {
    sema("la");
}
```

While loop:

```text
chombo a = 1;
mradi(a < 10) {
    sema(a);
    a = a + 1;
}
```

For loop:

```text
kwakila (chombo i = 0; i < 10; i++) {
    sema(i);
}
```

#### 8. Functions

Calling functions

```text
jumuisha(a, b);
```

Defining functions

```text
tendo jumuisha(a, b) {
    rudisha a + b;
}
```

Functions are first class in Swlang meaning they are real values that you can get a reference to, store in variables, pass around etc.

```text
tendo jumuisha(a, b) {
   return a + b;
}

tendo tokeo(a) {
    return a;
}

sema(tokeo(jumuisha)(1, 2)); // "3"
```

#### 9. Classes

Swlang is an object-oriented language featuring classes, inheritance, polymorphism etc. To create classes use the keyword ```ramani``` which is the swahili word for ```blueprint```. The reason I chose this name is because I think of classes as blueprints for an object with its properties and behaviours.

```text
ramani Mnyama {
    chombo jina;
    chombo miaka;
    
    tembea() {
        sema("Mimi natembea");
    }
    
    ongea() {
        sema("Mimi naongea");
    }
}

chombo ndovu = Mnyama();
ndovu.jina = "Ndovu";
ndovu.miaka = 20;
```

Swlang also supports inheritance.

```text
ramani Ndege hurithi Mnyama {
    prperuka() {
        sema("Mimi na peperuka.")
    }
}

chombo kanga = Ndege();
kanga.jina = "Kanga";
kanga.miaka = 22;
```

