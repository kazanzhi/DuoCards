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
    token?: string,
    message?: string
}

interface RegisterResponse {
    message?: string,
    errors?: string[]
}

export const authService = {
    async Register(registerData: RegisterData): Promise<RegisterResponse> {
        try {
            const response = await axios.post<RegisterResponse>(`${API_KEY}/register`, registerData)
            return response.data
        } catch (error: any) {
            if (error.response && error.response.data) {
                return error.response.data;
            } else {
                return { message: "An error occurred during registration" };
            }
        }
    },
    async Login(loginData: LoginData): Promise<LoginResponse> {
        try {
            const response = await axios.post<LoginResponse>(`${API_KEY}/login`, loginData)
            return response.data
        } catch (error: any) {
            if (error.response && error.response.data) {
                return error.response.data;
            } else {
                return { message: "An error occurred during login" };
            }
        }
    }
}