export interface Operation{
    debitOrCredit:boolean,
    otherSide:Number,
    amount:number,
    balance:Number,
    operationTime:Date
}