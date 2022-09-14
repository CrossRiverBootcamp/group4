export interface Operation{
    DebitOrCredit:boolean,
    otherSide:Number,
    amount:number,
    balance:Number,
    operationTime:Date
}