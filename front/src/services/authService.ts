import axios from "axios";

const API_KEY = "https://localhost:7108/api/Auth";

interface LoginData {
    email: string,
    password: string
}

interface RegisterData {
    email: string,
    password: string
}

interface LoginResponse {
    token: string
}

interface RegisterResponse {
    message: string
}

export const authService = {
    async Register(registerData: RegisterData): Promise<RegisterResponse> {
        const response = await axios.post<RegisterResponse>(`${API_KEY}/register`, registerData)
        return response.data
    },
    async Login(loginData: LoginData): Promise<LoginResponse> {
        const response = await axios.post<LoginResponse>(`${API_KEY}/login`, loginData)
        return response.data
    }
}