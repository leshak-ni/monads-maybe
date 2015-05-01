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

QUnit.test("with: null in chanin should return null", (assert) => {
    var _peopleWithUndefined: any = {};
    var peopleWithUndefined = _peopleWithUndefined;
    //action
    var name = maybe(peopleWithUndefined)
        .with(p=> p.address).with(p=> p.HouseName).value;
    //assert
    assert.equal(name, null);

});


QUnit.test("with: all props has values should return null", (assert) => {
    //action
    var name = maybe(peopleWithData)
        .with(p=> p.address).with(p=> p.HouseName).value;
    //assert
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