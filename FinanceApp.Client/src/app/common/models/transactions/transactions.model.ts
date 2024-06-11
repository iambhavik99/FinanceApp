export interface ITransactionResponseMedia {
    items: Transaction[];
}

export interface Transaction {
    transactionId: string;
    accountId: string;
    amount: number;
    description: string;
    transactionType: string;
}


export class TransactionResponseMedia implements ITransactionResponseMedia {
    items: Transaction[] = [];
}


export interface TransactionsRequestMedia {
    accountId: string;
    amount: string;
    description: string;
    transactionType: string;
}
