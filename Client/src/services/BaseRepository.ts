import { AxiosResponse } from "axios";
import { HttpClient } from "../utils/HttpClient";
const BASE_URL = import.meta.env.VITE_API_LOCAL_URL;
export interface IBaseRepository<T, S> {
  get(id: any): Promise<ApiResponse<T>>;
  getMany(): Promise<ApiResponse<T[]>>;
  search(searchParameters: S): Promise<ApiResponse<T[]>>;
  create(item: T): Promise<ApiResponse<T>>;
  update(id: any, item: T): Promise<ApiResponse<T>>;
  delete(id: any): Promise<ApiResponse<T>>;
}

export class ApiResponse<T> {
  data?: T;
  success?: boolean;
}

const transform = (response: AxiosResponse): Promise<ApiResponse<any>> => {

  return new Promise((resolve, reject) => {
    if (response != undefined) {
      const result: ApiResponse<any> = {
        data: response,
        success: response.status === 200,
      };
      resolve(result);
    }
    reject();
  });
};

export abstract class BaseRepository<T, S>
  extends HttpClient
  implements IBaseRepository<T, S>
{
  //Implementarea metodelor de baza pentru operatiuni CRUD
  public async search(searchParameters: S): Promise<ApiResponse<T[]>> {
    const instance = this.createInstance();
    const result = await instance
      .get(`${BASE_URL}/${this.collection}/filter`, {
        params: searchParameters,
      })
      .then(transform);
    return result as ApiResponse<T[]>;
  }
  public async get(id: any): Promise<ApiResponse<T>> {
    const instance = this.createInstance();
    const response = await instance
      .get(`${BASE_URL}/${this.collection}/${id}`)
    const result = await transform(response)
    return result as ApiResponse<T>;
  }
  public async getMany(): Promise<ApiResponse<T[]>> {
    const instance = this.createInstance();
    const response = await instance
      .get(`${BASE_URL}/${this.collection}`)
      
    const result = await transform(response)
    return result as ApiResponse<T[]>;
  }
  public async create(item: T): Promise<ApiResponse<T>> {
    const instance = this.createInstance();
    const result = await instance
      .post(`${BASE_URL}/${this.collection}`, item)
      .then(transform);
    return result as ApiResponse<T>;
  }
  public async update(id: any, item: T): Promise<ApiResponse<T>> {
    const instance = this.createInstance();
    const result = await instance
      .put(`${BASE_URL}/${this.collection}/${id}`, item)
      .then(transform);
    return result as ApiResponse<T>;
  }
  public async delete(id: any): Promise<ApiResponse<T>> {
    const instance = this.createInstance();
    const result = await instance
      .delete(`${BASE_URL}/${this.collection}/${id}`)
      .then(transform);
    return result as ApiResponse<T>;
  }
  protected collection: string | undefined;
}
