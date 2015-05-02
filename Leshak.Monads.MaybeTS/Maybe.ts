/// <reference path="libs/typed/qunit.d.ts" />
class Maybe<TInput> {

    public value: TInput;

    constructor(input: TInput) {
        this.value = input;
    }

    public with<TResult>(evaluator: (x: TInput) => TResult): Maybe<TResult> {
        if (this.value == null || typeof this.value == "undefined") return new Maybe(null);
        return new Maybe(evaluator(this.value));
    }

    public result<TResult>(evaluator: (x: TInput) => TResult,valueOnInputNull:TResult=null): Maybe<TResult> {
        if (this.value == null || typeof this.value == "undefined") return new Maybe(valueOnInputNull);
        return new Maybe(evaluator(this.value));
    }

    public default(valueOnInputNull: TInput):Maybe<TInput> { 
        // simular as Return, with default x=>x implementation of evaluator {
        if (this.value == null || typeof this.value == "undefined") return new Maybe(valueOnInputNull);
        return this;
    }

    public if(evaluator: (x: TInput) => boolean): Maybe<TInput> {
        if (this.value == null || typeof this.value == "undefined") return new Maybe(null);
        return evaluator(this.value) ? this: new Maybe(null);
    }

}

function maybe<TInput>(value: TInput) {// short alias of new Maybe
    return new Maybe(value);
}

