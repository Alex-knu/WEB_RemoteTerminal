import hashlib

def heshing(data):
    hash_data = hashlib.sha256(data.encode()).hexdigest().encode()
    return hash_data