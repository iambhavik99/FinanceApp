export interface ITransactionResponseMedia {
    items: Transaction[];
}

export interface Transaction {
    transactionId: string;
    categoryId: string;
    accountId: string;
    amount: number;
    note: string;
    transactionType: string;
    description: string;
    transactionDate: Date
}


export class TransactionResponseMedia implements ITransactionResponseMedia {
    items: Transaction[] = [];
}


export interface TransactionsRequestMedia {
    categoryId: string;
    accountId: string;
    amount: number;
    note: string;
    transactionType: string;
}


export enum TRANSACTION_TYPE {
    DEBIT = 'DEBIT',
    CREDIT = 'CREDIT'
}