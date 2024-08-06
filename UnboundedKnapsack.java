public class UnboundedKnapsack {

    // Hàm để tìm giá trị lớn nhất có thể đạt được và theo dõi các đồ vật được chọn
    public static void unboundedKnapsack(int W, int[] weights, int[] values, int n) {
        // Tạo bảng F để lưu giá trị lớn nhất có thể đạt được
        int[][] F = new int[n + 1][W + 1];
        // Tạo bảng X để lưu số lượng đồ vật được chọn
        int[][] X = new int[n + 1][W + 1];

        // Khởi tạo bảng F và X cho đồ vật đầu tiên
        for (int V = 0; V <= W; V++) {
            X[1][V] = V / weights[0];
            F[1][V] = X[1][V] * values[0];
        }

        // Tính toán bảng F và X cho các đồ vật còn lại
        for (int k = 2; k <= n; k++) {
            for (int V = 0; V <= W; V++) {
                F[k][V] = F[k - 1][V];
                X[k][V] = 0;
                for (int xk = 0; xk <= V / weights[k - 1]; xk++) {
                    int U = V - xk * weights[k - 1];
                    int value = F[k - 1][U] + xk * values[k - 1];
                    if (value > F[k][V]) {
                        F[k][V] = value;
                        X[k][V] = xk;
                    }
                }
            }
        }

        // In ra kết quả
        System.out.println("Gia tri lon nhat co the dat duoc: " + F[n][W]);
        System.out.println("So luong cac loai do vat duoc chon:");

        // Truy vết lại để tìm số lượng đồ vật được chọn
        int[] itemCount = new int[n];
        int remainingWeight = W;
        for (int k = n; k >= 1; k--) {
            itemCount[k - 1] = X[k][remainingWeight];
            remainingWeight -= X[k][remainingWeight] * weights[k - 1];
        }

        for (int i = 0; i < n; i++) {
            System.out.println("Loai " + (i + 1) + " (trong luong " + weights[i] + ", gia tri " + values[i] + "): " + itemCount[i]);
        }
    }

    public static void main(String[] args) {
        int W = 9; // Trọng lượng tối đa của ba lô
        int[] weights = {3, 4, 5, 2, 1}; // Trọng lượng của các loại đồ vật
        int[] values = {4, 5, 6, 3, 1}; // Giá trị của các loại đồ vật
        int n = weights.length; // Số loại đồ vật

        unboundedKnapsack(W, weights, values, n);
    }
}
