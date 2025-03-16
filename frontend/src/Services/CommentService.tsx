import axios from "axios";
import { CommentGet, CommentPost } from "../Models/Comment";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5227/api/comment/";

export const commentPostAPI = async (
  title: string,
  content: string,
  symbol: string
) => {
  try {
    const token = localStorage.getItem("token");

    if (!token) {
      console.error("No token found!");
      throw new Error("No authentication token available.");
    }

    const headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    };

    const data = await axios.post<CommentPost>(
      api + `${symbol}`,
      { title, content },
      { headers } // Explicitly pass headers
    );

    return data;
  } catch (error) {
    handleError(error);
  }
};

export const commentGetAPI = async (symbol: string) => {
  try {
    const token = localStorage.getItem("token");

    if (!token) {
      console.error("No token found!");
      throw new Error("No authentication token available.");
    }

    const headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    };

    const data = await axios.get<CommentGet[]>(
      api + `?Symbol=${symbol}`,
      { headers } // Explicitly pass headers
    );

    return data;
  } catch (error) {
    handleError(error);
  }
};
