import axios from "axios"
import { Card } from "../types/Card"
import { CardDto } from "../Dtos/CardDto"

const API_KEY = "https://localhost:7108/Card"

export const cardService = {
    async getAllCards(): Promise<Card[]> {
        const response = await axios.get<Card[]>(`${API_KEY}/get`)
        return response.data
    },
    async getById(id: number): Promise<Card> {
        const response = await axios.get<Card>(`${API_KEY}/get/${id}`)
        return response.data
    },
    async updateCard(id: number, cardDto: CardDto): Promise<Card> {
        const response = await axios.put<Card>(`${API_KEY}/update/${id}`, {...cardDto})
        return response.data
    },
    async createCard(cardDto: CardDto): Promise<Card> {
        const response = await axios.post<Card>(`${API_KEY}/create`, {...cardDto})
        return response.data
    },
    async correctAnswer(id: number): Promise<void> {
        const response = await axios.post<void>(`${API_KEY}/${id}/correct`)
        return response.data
    },
    async incorrectAnswer(id: number): Promise<void> {
        const response = await axios.post<void>(`${API_KEY}/${id}/incorrect`)
        return response.data
    },
    async translate(engWord: string): Promise<string> {
        const response = await axios.get<string>(`${API_KEY}/translate/${engWord}`)
        return response.data
    }
}