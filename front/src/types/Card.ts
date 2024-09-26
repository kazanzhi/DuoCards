import { CardStatus } from "../Enum/CardStatus"

export type Card = {
    id: number,
    ruWord: string,
    engWord: string,
    exampleOfUsage: string,
    cardStatus: CardStatus,
    imgUrl: string,
    successfulAttempts: number,
    reviewCount: number,
    nextReviewDate: Date
}

