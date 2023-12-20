/**
 * @fileoverview gRPC-Web generated client stub for Water
 * @enhanceable
 * @public
 */

// Code generated by protoc-gen-grpc-web. DO NOT EDIT.
// versions:
// 	protoc-gen-grpc-web v1.4.2
// 	protoc              v3.19.1
// source: char.proto


/* eslint-disable */
// @ts-nocheck


import * as grpcWeb from 'grpc-web';

import * as char_pb from './char_pb';


export class CharServiceClient {
  client_: grpcWeb.AbstractClientBase;
  hostname_: string;
  credentials_: null | { [index: string]: string; };
  options_: null | { [index: string]: any; };

  constructor (hostname: string,
               credentials?: null | { [index: string]: string; },
               options?: null | { [index: string]: any; }) {
    if (!options) options = {};
    if (!credentials) credentials = {};
    options['format'] = 'binary';

    this.client_ = new grpcWeb.GrpcWebClientBase(options);
    this.hostname_ = hostname.replace(/\/+$/, '');
    this.credentials_ = credentials;
    this.options_ = options;
  }

  methodDescriptorStart = new grpcWeb.MethodDescriptor(
    '/Water.CharService/Start',
    grpcWeb.MethodType.UNARY,
    char_pb.RequestRequest,
    char_pb.RequestResponse,
    (request: char_pb.RequestRequest) => {
      return request.serializeBinary();
    },
    char_pb.RequestResponse.deserializeBinary
  );

  start(
    request: char_pb.RequestRequest,
    metadata: grpcWeb.Metadata | null): Promise<char_pb.RequestResponse>;

  start(
    request: char_pb.RequestRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: char_pb.RequestResponse) => void): grpcWeb.ClientReadableStream<char_pb.RequestResponse>;

  start(
    request: char_pb.RequestRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: char_pb.RequestResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Water.CharService/Start',
        request,
        metadata || {},
        this.methodDescriptorStart,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Water.CharService/Start',
    request,
    metadata || {},
    this.methodDescriptorStart);
  }

  methodDescriptorCancel = new grpcWeb.MethodDescriptor(
    '/Water.CharService/Cancel',
    grpcWeb.MethodType.UNARY,
    char_pb.CancelRequest,
    char_pb.CancelResponse,
    (request: char_pb.CancelRequest) => {
      return request.serializeBinary();
    },
    char_pb.CancelResponse.deserializeBinary
  );

  cancel(
    request: char_pb.CancelRequest,
    metadata: grpcWeb.Metadata | null): Promise<char_pb.CancelResponse>;

  cancel(
    request: char_pb.CancelRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: char_pb.CancelResponse) => void): grpcWeb.ClientReadableStream<char_pb.CancelResponse>;

  cancel(
    request: char_pb.CancelRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: char_pb.CancelResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Water.CharService/Cancel',
        request,
        metadata || {},
        this.methodDescriptorCancel,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Water.CharService/Cancel',
    request,
    metadata || {},
    this.methodDescriptorCancel);
  }

  methodDescriptorAccept = new grpcWeb.MethodDescriptor(
    '/Water.CharService/Accept',
    grpcWeb.MethodType.UNARY,
    char_pb.AcceptOrRefuseRequest,
    char_pb.AcceptOrRefuseResult,
    (request: char_pb.AcceptOrRefuseRequest) => {
      return request.serializeBinary();
    },
    char_pb.AcceptOrRefuseResult.deserializeBinary
  );

  accept(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null): Promise<char_pb.AcceptOrRefuseResult>;

  accept(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: char_pb.AcceptOrRefuseResult) => void): grpcWeb.ClientReadableStream<char_pb.AcceptOrRefuseResult>;

  accept(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: char_pb.AcceptOrRefuseResult) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Water.CharService/Accept',
        request,
        metadata || {},
        this.methodDescriptorAccept,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Water.CharService/Accept',
    request,
    metadata || {},
    this.methodDescriptorAccept);
  }

  methodDescriptorRefuse = new grpcWeb.MethodDescriptor(
    '/Water.CharService/Refuse',
    grpcWeb.MethodType.UNARY,
    char_pb.AcceptOrRefuseRequest,
    char_pb.AcceptOrRefuseResult,
    (request: char_pb.AcceptOrRefuseRequest) => {
      return request.serializeBinary();
    },
    char_pb.AcceptOrRefuseResult.deserializeBinary
  );

  refuse(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null): Promise<char_pb.AcceptOrRefuseResult>;

  refuse(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: char_pb.AcceptOrRefuseResult) => void): grpcWeb.ClientReadableStream<char_pb.AcceptOrRefuseResult>;

  refuse(
    request: char_pb.AcceptOrRefuseRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: char_pb.AcceptOrRefuseResult) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Water.CharService/Refuse',
        request,
        metadata || {},
        this.methodDescriptorRefuse,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Water.CharService/Refuse',
    request,
    metadata || {},
    this.methodDescriptorRefuse);
  }

  methodDescriptorGetSession = new grpcWeb.MethodDescriptor(
    '/Water.CharService/GetSession',
    grpcWeb.MethodType.UNARY,
    char_pb.GetSessionRequest,
    char_pb.GetSessionResponse,
    (request: char_pb.GetSessionRequest) => {
      return request.serializeBinary();
    },
    char_pb.GetSessionResponse.deserializeBinary
  );

  getSession(
    request: char_pb.GetSessionRequest,
    metadata: grpcWeb.Metadata | null): Promise<char_pb.GetSessionResponse>;

  getSession(
    request: char_pb.GetSessionRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: char_pb.GetSessionResponse) => void): grpcWeb.ClientReadableStream<char_pb.GetSessionResponse>;

  getSession(
    request: char_pb.GetSessionRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: char_pb.GetSessionResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Water.CharService/GetSession',
        request,
        metadata || {},
        this.methodDescriptorGetSession,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Water.CharService/GetSession',
    request,
    metadata || {},
    this.methodDescriptorGetSession);
  }

  methodDescriptorMsg = new grpcWeb.MethodDescriptor(
    '/Water.CharService/Msg',
    grpcWeb.MethodType.SERVER_STREAMING,
    char_pb.EmptyRequest,
    char_pb.MsgResponse,
    (request: char_pb.EmptyRequest) => {
      return request.serializeBinary();
    },
    char_pb.MsgResponse.deserializeBinary
  );

  msg(
    request: char_pb.EmptyRequest,
    metadata?: grpcWeb.Metadata): grpcWeb.ClientReadableStream<char_pb.MsgResponse> {
    return this.client_.serverStreaming(
      this.hostname_ +
        '/Water.CharService/Msg',
      request,
      metadata || {},
      this.methodDescriptorMsg);
  }

}

