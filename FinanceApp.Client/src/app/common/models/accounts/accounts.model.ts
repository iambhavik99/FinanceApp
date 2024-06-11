export interface AccountResponseMedia {
    items: AccountsList[];
}

export interface AccountsList {
    accountId: string;
    accountName: string
    balance: number
}

export interface AccountRequestMedia {
    accountName: string;
    balance: number;
    userId: string;
}