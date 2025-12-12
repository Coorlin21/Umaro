import cv2
import mediapipe as mp
import numpy as np
import socket
import json

# 建立 UDP 傳送端
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
unity_address = ("127.0.0.1", 5005)  # Unity端接收位址與port

mp_pose = mp.solutions.pose
mp_drawing = mp.solutions.drawing_utils

cap = cv2.VideoCapture(0)
pose = mp_pose.Pose()

def get_angle(a, b, c):
    """計算三點夾角：b 為中心點"""
    a, b, c = np.array(a), np.array(b), np.array(c)
    ba = a - b
    bc = c - b
    cosine = np.dot(ba, bc) / (np.linalg.norm(ba) * np.linalg.norm(bc))
    return np.degrees(np.arccos(np.clip(cosine, -1.0, 1.0)))

def classify_pose(landmarks):
    # 平滑後的資料

    L_sh = landmarks[11]
    R_sh = landmarks[12]
    L_el = landmarks[13]
    R_el = landmarks[14]
    L_wr = landmarks[15]
    R_wr = landmarks[16]
    L_hip = landmarks[23]
    R_hip = landmarks[24]

    # 計算角度
    L_arm_angle = get_angle(L_wr, L_el, L_sh)
    R_arm_angle = get_angle(R_wr, R_el, R_sh)

    # 高度
    L_y = L_wr[1]; R_y = R_wr[1]
    L_sh_y = L_sh[1]; R_sh_y = R_sh[1]
    L_hip_y = L_hip[1]; R_hip_y = R_hip[1]

    # 水平方向(左右伸展)
    L_x = L_wr[0]; R_x = R_wr[0]
    L_sh_x = L_sh[0]; R_sh_x = R_sh[0]
    L_hip_x = L_hip[0]; R_hip_x = R_hip[0]

    # ====== 姿勢分類 ======

    # 1. 雙手上舉
    if L_y < L_sh_y - 0.2 and R_y < R_sh_y - 0.2:
            return 1

    # 2. T pose（高度接近 + 水平方向明顯）
    if abs(L_y - L_sh_y) < 0.07 and abs(R_y - R_sh_y) < 0.07:
        if abs(L_x - L_sh_x) > 0.15 and abs(R_x - R_sh_x) > 0.15:
            return 2

    # 3. 舉手90度
    if L_y < L_sh_y and R_y < R_sh_y:
        return 3

    # 4. 叉腰
    if L_y - L_hip_y < -0.10 and R_y - R_hip_y < -0.10:
        if abs(L_x - L_hip_x)< 0.1 and abs(R_x - R_hip_x)< 0.1: 
            return 4

    return 0

    
    
def classify_positon(landmarks):
    left_shoulder = landmarks[11]
    right_shoulder = landmarks[12]

    # 計算身體中心
    body_center_x = (left_shoulder[0] + right_shoulder[0]) / 2

    if body_center_x < 0.4:
        return -1  # 左移
    elif body_center_x > 0.6:
        return 1  # 右移
    else :
        return 0  #中間
    
def classify_vertical(landmarks):
    L_hip = landmarks[23]
    R_hip = landmarks[24]
    body_y = (L_hip[1] + R_hip[1]) / 2

    if body_y < 0.55:
        return 1  # 跳躍
    elif body_y > 0.7:
        return -1   # 蹲下
    else :
        return 0
    

    prev_nose_y = nose_y


while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break
    frame = cv2.flip(frame, 1)
    rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    results = pose.process(rgb)

    if results.pose_landmarks:
        landmarks = [(lm.x, lm.y, lm.z) for lm in results.pose_landmarks.landmark]
        pose_id = classify_pose(landmarks)
        position_id = classify_positon(landmarks)
        vertical_id = classify_vertical(landmarks)

        # 傳送給Unity
        message = json.dumps({"pose_id": pose_id,"position_id":position_id,"vertical_id":vertical_id})
        sock.sendto(message.encode(), unity_address)


        mp_drawing.draw_landmarks(frame, results.pose_landmarks, mp_pose.POSE_CONNECTIONS)
    cv2.imshow('Pose Detection', frame)
    if cv2.waitKey(1) & 0xFF == 27:
        break

cap.release()
cv2.destroyAllWindows()