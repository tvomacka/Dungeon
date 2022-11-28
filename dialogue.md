# Dialogue
## Conditions
### Definition
Each condition is defined by *Subject*, *Test* and *Target*.  
The *Subject* determines what will be tested, such as character attribute, item or quest state, etc.  
The *Target* determines the value against which it will be tested.  
The *Test* determines the condition which needs to be satisfied for the test to succeed.  
For example, if (*Subject*, *Target*, *Test*) is (Intelligence, 10, GreaterThan) then the condition is satisfied if the character's INT > 10.
### Test Matrix
|Condition|Attributes|Item|
|:---:|:---:|:---:|
|GreaterThan|X|-|
|InInventory|-|X|

<details><summary>Details</summary>
  <p> The allowed attributes are "Intelligence".</p>
</details>
