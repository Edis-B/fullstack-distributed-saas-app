/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { LoginDto } from '../models/LoginDto';
import type { RegisterDto } from '../models/RegisterDto';
import type { UserDataDto } from '../models/UserDataDto';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class AuthService {
    /**
     * @param requestBody
     * @returns void
     * @throws ApiError
     */
    public static postLogin(
        requestBody?: LoginDto,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/login',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns void
     * @throws ApiError
     */
    public static postRegister(
        requestBody?: RegisterDto,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/register',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized`,
            },
        });
    }
    /**
     * @returns UserDataDto OK
     * @throws ApiError
     */
    public static getStatus(): CancelablePromise<UserDataDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/status',
        });
    }
}
