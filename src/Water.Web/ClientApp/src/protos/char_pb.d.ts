import * as jspb from 'google-protobuf'

import * as google_protobuf_timestamp_pb from 'google-protobuf/google/protobuf/timestamp_pb';
import * as google_protobuf_duration_pb from 'google-protobuf/google/protobuf/duration_pb';


export class EmptyRequest extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): EmptyRequest.AsObject;
  static toObject(includeInstance: boolean, msg: EmptyRequest): EmptyRequest.AsObject;
  static serializeBinaryToWriter(message: EmptyRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): EmptyRequest;
  static deserializeBinaryFromReader(message: EmptyRequest, reader: jspb.BinaryReader): EmptyRequest;
}

export namespace EmptyRequest {
  export type AsObject = {
  }
}

export class RequestRequest extends jspb.Message {
  getName(): string;
  setName(value: string): RequestRequest;

  getWithtext(): string;
  setWithtext(value: string): RequestRequest;

  getDeadline(): google_protobuf_duration_pb.Duration | undefined;
  setDeadline(value?: google_protobuf_duration_pb.Duration): RequestRequest;
  hasDeadline(): boolean;
  clearDeadline(): RequestRequest;

  getStarttime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setStarttime(value?: google_protobuf_timestamp_pb.Timestamp): RequestRequest;
  hasStarttime(): boolean;
  clearStarttime(): RequestRequest;

  getResultid(): number;
  setResultid(value: number): RequestRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RequestRequest.AsObject;
  static toObject(includeInstance: boolean, msg: RequestRequest): RequestRequest.AsObject;
  static serializeBinaryToWriter(message: RequestRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RequestRequest;
  static deserializeBinaryFromReader(message: RequestRequest, reader: jspb.BinaryReader): RequestRequest;
}

export namespace RequestRequest {
  export type AsObject = {
    name: string,
    withtext: string,
    deadline?: google_protobuf_duration_pb.Duration.AsObject,
    starttime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    resultid: number,
  }
}

export class EndRequestData extends jspb.Message {
  getRequest(): RequestRequest | undefined;
  setRequest(value?: RequestRequest): EndRequestData;
  hasRequest(): boolean;
  clearRequest(): EndRequestData;

  getJoinedList(): Array<string>;
  setJoinedList(value: Array<string>): EndRequestData;
  clearJoinedList(): EndRequestData;
  addJoined(value: string, index?: number): EndRequestData;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): EndRequestData.AsObject;
  static toObject(includeInstance: boolean, msg: EndRequestData): EndRequestData.AsObject;
  static serializeBinaryToWriter(message: EndRequestData, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): EndRequestData;
  static deserializeBinaryFromReader(message: EndRequestData, reader: jspb.BinaryReader): EndRequestData;
}

export namespace EndRequestData {
  export type AsObject = {
    request?: RequestRequest.AsObject,
    joinedList: Array<string>,
  }
}

export class RequestResponse extends jspb.Message {
  getResultid(): number;
  setResultid(value: number): RequestResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RequestResponse.AsObject;
  static toObject(includeInstance: boolean, msg: RequestResponse): RequestResponse.AsObject;
  static serializeBinaryToWriter(message: RequestResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RequestResponse;
  static deserializeBinaryFromReader(message: RequestResponse, reader: jspb.BinaryReader): RequestResponse;
}

export namespace RequestResponse {
  export type AsObject = {
    resultid: number,
  }
}

export class CancelRequest extends jspb.Message {
  getName(): string;
  setName(value: string): CancelRequest;

  getResultid(): number;
  setResultid(value: number): CancelRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CancelRequest.AsObject;
  static toObject(includeInstance: boolean, msg: CancelRequest): CancelRequest.AsObject;
  static serializeBinaryToWriter(message: CancelRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CancelRequest;
  static deserializeBinaryFromReader(message: CancelRequest, reader: jspb.BinaryReader): CancelRequest;
}

export namespace CancelRequest {
  export type AsObject = {
    name: string,
    resultid: number,
  }
}

export class CancelResponse extends jspb.Message {
  getResultid(): number;
  setResultid(value: number): CancelResponse;

  getResult(): boolean;
  setResult(value: boolean): CancelResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CancelResponse.AsObject;
  static toObject(includeInstance: boolean, msg: CancelResponse): CancelResponse.AsObject;
  static serializeBinaryToWriter(message: CancelResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CancelResponse;
  static deserializeBinaryFromReader(message: CancelResponse, reader: jspb.BinaryReader): CancelResponse;
}

export namespace CancelResponse {
  export type AsObject = {
    resultid: number,
    result: boolean,
  }
}

export class AcceptOrRefuseRequest extends jspb.Message {
  getName(): string;
  setName(value: string): AcceptOrRefuseRequest;

  getResultid(): number;
  setResultid(value: number): AcceptOrRefuseRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AcceptOrRefuseRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AcceptOrRefuseRequest): AcceptOrRefuseRequest.AsObject;
  static serializeBinaryToWriter(message: AcceptOrRefuseRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AcceptOrRefuseRequest;
  static deserializeBinaryFromReader(message: AcceptOrRefuseRequest, reader: jspb.BinaryReader): AcceptOrRefuseRequest;
}

export namespace AcceptOrRefuseRequest {
  export type AsObject = {
    name: string,
    resultid: number,
  }
}

export class AcceptOrRefuseResult extends jspb.Message {
  getName(): string;
  setName(value: string): AcceptOrRefuseResult;

  getResultid(): number;
  setResultid(value: number): AcceptOrRefuseResult;

  getTotalcount(): number;
  setTotalcount(value: number): AcceptOrRefuseResult;

  getResulttype(): AcceptResultType;
  setResulttype(value: AcceptResultType): AcceptOrRefuseResult;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AcceptOrRefuseResult.AsObject;
  static toObject(includeInstance: boolean, msg: AcceptOrRefuseResult): AcceptOrRefuseResult.AsObject;
  static serializeBinaryToWriter(message: AcceptOrRefuseResult, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AcceptOrRefuseResult;
  static deserializeBinaryFromReader(message: AcceptOrRefuseResult, reader: jspb.BinaryReader): AcceptOrRefuseResult;
}

export namespace AcceptOrRefuseResult {
  export type AsObject = {
    name: string,
    resultid: number,
    totalcount: number,
    resulttype: AcceptResultType,
  }
}

export class MsgResponse extends jspb.Message {
  getType(): MsgType;
  setType(value: MsgType): MsgResponse;

  getBody(): Uint8Array | string;
  getBody_asU8(): Uint8Array;
  getBody_asB64(): string;
  setBody(value: Uint8Array | string): MsgResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): MsgResponse.AsObject;
  static toObject(includeInstance: boolean, msg: MsgResponse): MsgResponse.AsObject;
  static serializeBinaryToWriter(message: MsgResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): MsgResponse;
  static deserializeBinaryFromReader(message: MsgResponse, reader: jspb.BinaryReader): MsgResponse;
}

export namespace MsgResponse {
  export type AsObject = {
    type: MsgType,
    body: Uint8Array | string,
  }
}

export class GetSessionRequest extends jspb.Message {
  getResultid(): number;
  setResultid(value: number): GetSessionRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetSessionRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetSessionRequest): GetSessionRequest.AsObject;
  static serializeBinaryToWriter(message: GetSessionRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetSessionRequest;
  static deserializeBinaryFromReader(message: GetSessionRequest, reader: jspb.BinaryReader): GetSessionRequest;
}

export namespace GetSessionRequest {
  export type AsObject = {
    resultid: number,
  }
}

export class GetSessionResponse extends jspb.Message {
  getId(): number;
  setId(value: number): GetSessionResponse;

  getOwner(): string;
  setOwner(value: string): GetSessionResponse;

  getWithtext(): string;
  setWithtext(value: string): GetSessionResponse;

  getStarttime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setStarttime(value?: google_protobuf_timestamp_pb.Timestamp): GetSessionResponse;
  hasStarttime(): boolean;
  clearStarttime(): GetSessionResponse;

  getState(): GWaterSessionState;
  setState(value: GWaterSessionState): GetSessionResponse;

  getJoinedList(): Array<string>;
  setJoinedList(value: Array<string>): GetSessionResponse;
  clearJoinedList(): GetSessionResponse;
  addJoined(value: string, index?: number): GetSessionResponse;

  getSucceed(): boolean;
  setSucceed(value: boolean): GetSessionResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetSessionResponse.AsObject;
  static toObject(includeInstance: boolean, msg: GetSessionResponse): GetSessionResponse.AsObject;
  static serializeBinaryToWriter(message: GetSessionResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetSessionResponse;
  static deserializeBinaryFromReader(message: GetSessionResponse, reader: jspb.BinaryReader): GetSessionResponse;
}

export namespace GetSessionResponse {
  export type AsObject = {
    id: number,
    owner: string,
    withtext: string,
    starttime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    state: GWaterSessionState,
    joinedList: Array<string>,
    succeed: boolean,
  }
}

export enum AcceptResultType { 
  SUCCEED = 0,
  NOSESSION = 1,
  ALREADYJOINED = 2,
}
export enum MsgType { 
  START = 0,
  CANCEL = 1,
  ACCEPT = 2,
  REFUSE = 3,
  ENDOFSESSION = 4,
}
export enum GWaterSessionState { 
  WAITINGSTATE = 0,
  RUNNINGSTATE = 1,
  CANCELSTATE = 2,
  ENDSTATE = 3,
}
