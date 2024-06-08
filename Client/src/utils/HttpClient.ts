import axios, {AxiosError, AxiosInstance, AxiosResponse} from "axios";

export abstract class HttpClient{
    protected instance: AxiosInstance | undefined;
    protected createInstance(): AxiosInstance{
        this.instance = axios.create({
            baseURL: import.meta.env.VITE_API_LOCAL_URL,
            headers: {
                "Content-Type": "application/json",
            },
        });
        this.initializeResponseReceptor();
        return this.instance;
    }

    private initializeResponseReceptor =() => {
        this.instance?.interceptors.response.use(this.handleResponse, this.handleError)
        var token = localStorage.getItem("token")
        this.instance?.interceptors.request.use((config:any)=>{
            config.headers = {
                Authorization: `Bearer ${token}`,
              };
            return config
        })
    }
    private handleResponse = ({data}: AxiosResponse)=> data

    private handleError = async (error:AxiosError) => {
        const originalRequest = error.config;

        if(error.response?.status === 401){
            console.log("unauth");
            
        }
    }

}