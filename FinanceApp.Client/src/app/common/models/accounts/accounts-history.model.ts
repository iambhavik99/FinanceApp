export class AccountTransactionHistoryResponseMedia {
    items: AccountTransactionMedia[] = [];
    records: AccountTransactionPreviewMedia[] = [];
}

export class AccountTransactionMedia {
    transactionId!: string;
    accountId!: string;
    categoryName!: string;
    type!: number;
    description!: string;
    amount!: string;
    balance!: string;
    timestamp!: number;
}

export class AccountTransactionPreviewMedia {
    balance!: string;
    timestamp!: number;
}