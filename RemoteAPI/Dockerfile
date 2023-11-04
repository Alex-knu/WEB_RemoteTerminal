FROM python:3.9.6-alpine3.14

WORKDIR /app

COPY ./ /app

RUN pip install -r /app/requirements.txt \
	&& apk add sshpass

CMD ["python", "/app/main.py"]

EXPOSE 5000
