import axios, { AxiosResponse } from "axios";

axios.defaults.baseURL = 'https://localhost:7094/api/'
const responseBody = (response: AxiosResponse) => response.data

const request = {
    get: (url: string) => axios.get(url).then(responseBody)
}

const CardContent = {
    list: () => request.get('https://localhost:7094/api/Card')
}

const agent = {
    CardContent
}

export default agent