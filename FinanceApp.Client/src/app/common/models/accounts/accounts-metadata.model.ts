export class AccountMetadataMedia {
    totalBalance?: number;
    totalExpance?: number;
    totalIncome?: number;
    transactions: TransactionByMonthsMedia[] = [];
    expanses: AccountExpanses[] = []
}

export class AccountMetadata {
    name?: string;
    totalBalance?: number;
}

export class AccountExpanses {
    categoryName!: string;
    amount!: number;
}

export class TransactionByMonthsMedia {
    income!: number;
    expanse!: number;
    month!: string;
}