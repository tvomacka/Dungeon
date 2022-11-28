# Dialogue
## Conditions
### Definition
Each condition is defined by *Subject*, *Test* and *Target*.  
 - The *Subject* determines what will be tested, such as character attribute, item or quest state, etc.  
 - The *Target* determines the value against which it will be tested.  
 - The *Test* determines the condition which needs to be satisfied for the test to succeed.  
 
For example, if (*Subject*, *Target*, *Test*) is (Intelligence, 10, GreaterThan) then the condition is satisfied if the character's INT > 10.  
*Subject* and *Test* are not case sensitive, you can use (INTELLIGENCE, intelligence, or any other combination of upper/lower case letters).
*Test* will often be parsed as a number type and must follow the required formatting. If used as a string, it is case-insensitive as well.  

### Test Matrix
|Condition|Attributes|Item|Quest|
|:---:|:---:|:---:|:---:|
|GreaterThan|X|-|-|
|GreaterThanOrEqual|X|-|-|
|LessThan|X|-|-|
|LessThanOrEqual|X|-|-|
|Equal|X|-|-|
|NotEqual|X|-|-|
|InInventory|-|X|-|
|Assigned|-|-|X|
|Unassigned|-|-|X|
|Completed|-|-|X|
|Failed|-|-|X|

<details><summary>Details</summary>
  <p> The allowed attributes are "Intelligence".</p>
</details>
