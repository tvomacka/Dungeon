# Dialogue

Dialogue traverses through individual states, each state consists of a text and a list of options.
Each option can have a condition that determines if it is shown to the player (and therefore choosable by the player).
Each option can have actions attached to it, which will be executed before traversing to the next dialogue state.  

## Conditions

### Definition

Each condition is defined by *Subject*, *Test* and *Target*.  
 - The *Subject* determines what will be tested, such as character attribute, item or quest state, etc.  
 - The *Target* determines the value against which it will be tested.  
 - The *Test* determines the condition which needs to be satisfied for the test to succeed.  

<details><summary>Details</summary>

For example, if (*Subject*, *Target*, *Test*) is (Intelligence, 10, GreaterThan) then the condition is satisfied if the character's INT > 10.  
*Subject* and *Test* are not case sensitive, you can use (INTELLIGENCE, intelligence, or any other combination of upper/lower case letters).
*Test* will often be parsed as a number type and must follow the required formatting. If used as a string, it is case-insensitive as well.  
 
 </details>

### Test Matrix

|Condition|Attributes|Inventory|AssignedQuests|PartyMembers|
|:---:|:---:|:---:|:---:|:---:|
|GreaterThan|X|-|-|X|
|GreaterThanOrEqual|X|-|-|X|
|LessThan|X|-|-|X|
|LessThanOrEqual|X|-|-|X|
|Equal|X|-|-|X|
|NotEqual|X|-|-|X|
|Contains|-|X|X|X|

<details><summary>Details</summary>
 
  The allowed attributes are "Intelligence".
 
</details>

### Compound Conditions

TODO...

## Actions

TODO...

## Dialogue Editor

TODO...
