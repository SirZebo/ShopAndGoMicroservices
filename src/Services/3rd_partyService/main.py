from fastapi import FastAPI, HTTPException, Request
from pydantic import BaseModel
import requests
from requests import request
import base64
import hashlib
import hmac
from cryptography.hazmat.primitives import serialization
from cryptography.hazmat.primitives.asymmetric import padding
from cryptography.hazmat.primitives import hashes 
from fastapi.middleware.cors import CORSMiddleware


app = FastAPI()

PUBLIC_KEY_PEM = """-----BEGIN PUBLIC KEY-----
MIIBojANBgkqhkiG9w0BAQEFAAOCAY8AMIIBigKCAYEAsAxycc+u8RTqrflOIzI1
0Nukn1ly6QK+gHHPDc59f8uUQxrKwU2G+jFX5cBol3Vgj7DAmir3A7kvm5pELLD/
/jB1rryVc4tF00/fTYkyfD6G5rUcGCCyaB2RYPJNAB5wJ/r8x4bEvsGiNK2pqOPw
9ziLKxmN99SvXsRKHrFWVH5yH3+z+b77GSL20WqUk828HeY/UWkTvy2lYsp8sNPx
R5bxJXP0jXZhM9oOp8RCP2OkGL6d/rohbR6+UR1nZAL2oGca/Y4RY3XG/5B0Cy67
EHv/MFQnjVWbmVDfklws+LwE6dNGe6of0BS7d6EQnsFiigmc+F7i+f8fzUBefIU8
Xtm9VjZkFs2ksCAp0fEsVbsi4Xvy9uR7CWW+3R8aHxCljyDd8mYHJEa6hOTmBGmK
Jt2D7tycDkcAyH0Eol37VWGWcYt67t2sDJBFfC1Fuxj4VpuqfLMb/6MrYEyYlCbE
zwD/r4L5+nLTaCyu7ZPqsg5V+YylUeG83J052ppwGSYnAgMBAAE=
-----END PUBLIC KEY-----"""

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Allows all origins (not recommended for production)
    allow_credentials=True,
    allow_methods=["*"],  # Allows all HTTP methods
    allow_headers=["*"],  # Allows all headers
)
def verify_signature(payload: str, signature: str) -> bool:
    try:
        public_key = serialization.load_pem_public_key(PUBLIC_KEY_PEM.encode())
        signature_bytes = base64.b64decode(signature)
        public_key.verify(
            signature_bytes,
            payload.encode(),
            padding.PKCS1v15(),
            hashes.SHA256()
        )
        return True
    except Exception as e:
        print(f"Signature verification failed: {e}")
        return False
    
class CreatePurchaseDto(BaseModel):
    paymentId: str
    transactionToken: str
    transactionAmount: float

class PurchaseSuccessDto(BaseModel):
    paymentId: str

# Define the schema for the incoming JSON payload
class PurchaseCallback(BaseModel):
    transaction_token: str
    event_type: str
    status: str

transactionToken_cache = {
    "efcb42e3-8883-4197-ac21-a82a9ddcbcf4": {
        "paymentId": "efcb42e3-8883-4197-ac21-a82a9ddcbcf4",
        "purchaseUrl": "https://gate.chip-in.asia/p/456e3e62-dc9b-42e0-8e91-cab5a20266e6/",
        "message": "",
        "status": "pending"
    },
    "efcb42e3-8883-4197-ac21-3123nbioasd": {
        "paymentId": "efcb42e3-8883-4197-ac21-3123nbioasd",
        "purchaseUrl": "",
        "message": "already paid from wallet",
        "status": "success"
    }
}

@app.get("/getPurchaseUrl")
def get_purchase_url(transactionToken: str):
    print("Starting get_purchase_url function...")
    #todo
    # 1. check cache d
    # 2. if found, return purchaseUrl, message (happens when amount is 0 so no url to return)
    # 3. if not found, return error message
    
    # Simulate checking the token in a database
    for tokenid, token in transactionToken_cache.items():
        if tokenid == transactionToken:
            print("Transaction token found in cache")  
            return {
                "paymentId": token["paymentId"],
                "purchaseUrl": token["purchaseUrl"],
                "message": token["message"],
                "status": token["status"]
            }
    return {"error": "Transaction token not found"}

@app.post("/createPurchase")
def create_purchase(dto: CreatePurchaseDto):
    print("Starting create_purchase function...")  # Debugging print

    if dto.transactionAmount < 0:
        raise HTTPException(status_code=400, detail="Transaction amount cannot be negative")
    
    if dto.transactionAmount == 0:
        transactionToken_cache[dto.transactionToken] = {
            "paymentId": dto.paymentId,
            "purchaseUrl": "",
            "message": "already paid from wallet",
            "status": "success"
        }
        return transactionToken_cache[dto.transactionToken]

    url = "https://gate.chip-in.asia/api/v1/purchases/"
    amount = int(dto.transactionAmount * 100)
    payload = {
        "client": {"email": "service@dashapp.asia"},
        "purchase": {
            "products": [
                {
                    "name": dto.transactionToken,
                    "price": amount,
                }
            ],
            "currency": "SGD"
        },
        "brand_id": "0579c452-295d-4048-a277-1d28946ffa0f",
        "payment_method_whitelist": ["visa", "mastercard"],
    }

    headers = {
        "Authorization": "Bearer nAzoWQCXaR04dtGVHh5iCcDpH6-sEKjZx5WbbDJoyKAbraK1MARW7y1FdRXCoHcK9vxB1BqJ3nLiOUndgIzSWg==",
        "Content-Type": "application/json"
    }

    print("Sending request to Chip-in API...")  # Debugging print
    response = requests.post(url, json=payload, headers=headers)
    print(f"Chip-in API response status code: {response.status_code}")  # Debugging print
    print(f"Chip-in API response body: {response.text}") # Debugging print

    purchase_url = response.json().get("checkout_url", "") if response.status_code == 201 else ""

    transactionToken_cache[dto.transactionToken] = {
        "paymentId": dto.paymentId,
        "purchaseUrl": purchase_url,
        "message": "",
        "status": "pending"
    }

    print("Purchase details saved in cache.")
    return transactionToken_cache[dto.transactionToken]

@app.post("/success_callback")
def purchase_success_callback(dto: PurchaseSuccessDto):
    print("Starting purchase_success_callback function...")
    #dto.transactionToken 
    #post into an endpoint to update the status of the transaction token
    #body 
    payload = {
        "paymentId": dto.paymentId 
    }
    url ="https://localhost:6061/payments/receive"
    
    response = requests.post(url, json=payload)
    return response.json()

# Define the POST endpoint to handle the callback
@app.post("/chip_success_callback")
async def chip_success_callback(callback: PurchaseCallback):
    print("Starting chip_success_callback function...")
    print(f"Received transaction_token: {callback.transaction_token}")
    print(f"Received event_type: {callback.event_type}")
    print(f"Received status: {callback.status}")
    
    # Optionally, you could validate the signature here

    return {"message": "Callback received successfully", "status": "success"
    
# @app.post("/success_callback")                                                            
# async def purchase_success_callback(request: Request):
#     body = await request.body()
#     signature = request.headers.get("X-Signature")
    
#     if not signature:
#         raise HTTPException(status_code=400, detail="Missing signature header")
    
#     if not verify_signature(body.decode(), signature):
#         raise HTTPException(status_code=400, detail="Invalid signature")
    
#     purchase_data = json.loads(body)
#     event_type = purchase_data.get("event_type")
    
#     print(f"Received event: {event_type}")
#     print(f"Purchase data: {purchase_data}")
    
#     return {"message": "Callback received successfully"}



