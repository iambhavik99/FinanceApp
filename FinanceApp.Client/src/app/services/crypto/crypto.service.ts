import { Injectable } from '@angular/core';
import { Base64 } from 'js-base64';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

  constructor() { }

  /** import base64 key string and import it into CryptoKey format */
  async importKey(keyBase64: any): Promise<CryptoKey> {
    return new Promise((resolve, reject) => {
      const binaryData = Base64.toUint8Array(keyBase64) as Uint8Array;

      crypto.subtle.importKey(
        'raw',
        binaryData,
        { name: "AES-GCM" },
        true,
        ['encrypt', 'decrypt']
      )
        .then(key => resolve(key))
        .catch(err => reject(err))
    });
  }

  /** encrypt plain text data and provide encrypted string with initial vector */
  async encrypt(key: CryptoKey, ivString: string, plaintextString: string): Promise<string> {
    return new Promise((resolve, reject) => {

      const encoder = new TextEncoder();
      const data = encoder.encode(plaintextString);
      const iv = Base64.toUint8Array(ivString);

      const algorithm = {
        name: 'AES-GCM',
        iv: iv
      };

      crypto.subtle.encrypt(algorithm, key, data)
        .then(response => resolve(Base64.fromUint8Array(new Uint8Array(response))))
        .catch(err => reject(err))
    });
  }


}
