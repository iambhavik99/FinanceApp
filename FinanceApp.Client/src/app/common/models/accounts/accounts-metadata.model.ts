export class AccountMetadataMedia {
    totalBalance?: number;
    totalExpance?: number;
    totalIncome?: number;
    accounts: AccountMetadata[] = [];
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