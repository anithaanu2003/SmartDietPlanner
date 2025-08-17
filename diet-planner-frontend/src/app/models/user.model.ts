export interface RegisterRequest {
  email: string;
  password: string;
}

export interface RegisterResponse {
  token: string;
}
