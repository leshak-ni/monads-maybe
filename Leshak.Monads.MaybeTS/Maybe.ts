/// <reference path="libs/typed/qunit.d.ts" />
class Maybe<TInput> {

    public value: TInput;

    constructor(input: TInput) {
        this.value = input;
        if (typeof this.value === "undefined") { this.value = null; }// normalize "undefined" to null
    }

    public with<TResult>(evaluator: (x: TInput) => TResult): Maybe<TResult> {
        if (this.value === null) { return new Maybe(null); }
        return new Maybe(evaluator(this.value));
    }

    public result<TResult>(evaluator: (x: TInput) => TResult,valueOnInputNull:TResult=null): Maybe<TResult> {
        if (this.value === null) { return new Maybe(valueOnInputNull); }
        return new Maybe(evaluator(this.value));
    }

    public default(valueOnInputNull: TInput):Maybe<TInput> {
        // simular as Return, with default x=>x implementation of evaluator {
        if (this.value === null) { return new Maybe(valueOnInputNull); }
        return this;
    }

    public if(evaluator: (x: TInput) => boolean): Maybe<TInput> {
        if (this.value === null) { return new Maybe(null); }
        return evaluator(this.value) ? this: new Maybe(null);
    }

    public do(action: (x: TInput) => void): Maybe<TInput>  {
        if (this.value === null) { return new Maybe(null); }
        action(this.value);
        return this;
    }

}
/* tslint:disable:no-unused-variable */
function maybe<TInput>(value: TInput) {// short alias of new Maybe
    return new Maybe(value);
}
/* tslint:enable:no-unused-variable */
