/// <reference path="maybe.ts" />
/// <reference path="libs/typed/qunit.d.ts" />



//test data
var peopleWithNull: People;
var peopleWithData: People;


QUnit.module("Maybe", {
    setup: function () {
        peopleWithNull = new People();

        peopleWithData = new People();
        peopleWithData.address = new Address();
        peopleWithData.address.HouseName = "Some Name";
        
    }
});


//#region with Method
QUnit.test("with: input=null should return null", (assert) => {
    var people:People = null;
    var pm = new Maybe(people);

    var name = maybe(people)
        .with(p=> p.address).with(p=> p.HouseName).value;

    //assert
    assert.equal(name, null);

});

QUnit.test("with: null in chanin should return null", (assert) => {
    //action
    var name = maybe(peopleWithNull)
        .with(p=> p.address).with(p=> p.HouseName).value;
    //assert
    assert.equal(name, null);

});

QUnit.test("with: null in chain should return null", (assert) => {
    var _peopleWithUndefined: any = {};
    var peopleWithUndefined = _peopleWithUndefined;
    //action
    var name = maybe(peopleWithUndefined)
        .with(p=> p.address).with(p=> p.HouseName).value;
    //assert
    assert.equal(name, null);

});


QUnit.test("with: all props has values should return value", (assert) => {
    //action
    var name = maybe(peopleWithData)
        .with(p=> p.address).with(p=> p.HouseName).value;
    //assert
    assert.equal(name, "Some Name");

});
//#endregion


//#region Result
QUnit.test("result: null in chain should return 'default value'", (assert) => {
    //action
    var name = maybe(peopleWithNull)
        .with(p=> p.address).with(p=> p.HouseName)
        .result(x=>x,"default value").value;
    //assert
    assert.equal(name, 'default value');

});

QUnit.test("result: all props has values should return value", (assert) => {
    //action
    var name = maybe(peopleWithData)
        .with(p=> p.address).with(p=> p.HouseName)
        .result(x=> x, "default value").value;
    //assert
    assert.equal(name, "Some Name");

});
//#endregion

//#region Default
QUnit.test("default: all props has values, should return value", (assert) => {
    //action
    var name = maybe(peopleWithData)
        .with(p=> p.address).with(p=> p.HouseName)
        .default( "default value").value; // should ingnored
    //assert
    assert.equal(name, "Some Name");

});

QUnit.test("default: target property is null, should return 'default value'", (assert) => {
    //arrange
    peopleWithData.address.HouseName = null;// 'targetPropperty' not set
    //action
    var name = maybe(peopleWithData)
        .with(p=> p.address).with(p=> p.HouseName)
        .default("default value").value;
    //assert
    assert.equal(name, "default value");

});


QUnit.test("default: null in chain, should return 'default value'", (assert) => {
    //action
    var name = maybe(peopleWithNull)
        .with(p=> p.address).with(p=> p.HouseName)
        .default("default value").value;
    //assert
    assert.equal(name, "default value");

});
//#endregion

//#region If


QUnit.test("if: null in chain, should return null (if - ignored)", (assert) => {
    
    var name = maybe(peopleWithNull)
        .with(p=> p.address) // it's null
        //action
        .if(p=> true) // evaluator return true,but input is null 
        //assert
        .with(p=>p.HouseName).value;
    
    assert.equal(name, null);

});


QUnit.test("if: evaluator return false, should return null ", (assert) => {
    
    var name = maybe(peopleWithData)
        .with(p=> p.address) // it's not null
        //action
        .if(p=> p.HouseName.length < 3) // input has value, but evaluator return false
        //assert
        .with(p=> p.HouseName).value;
    
    assert.equal(name, null);

});


QUnit.test("if: evaluator return true and value not null, should return value ", (assert) => {

    var name = maybe(peopleWithData)
        .with(p=> p.address) // it's not null
    //action
        .if(p=> p.HouseName.length > 3) // input has value and evaluator return true
    //assert
        .with(p=> p.HouseName).value;

    assert.equal(name, "Some Name");

});

//#endregion

//#region DataClasses
class People {
    public address: Address ;
}

class Address {
    public HouseName: string;
}
//#endregion