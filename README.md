
# 🏭 Warehouse Simulator

A console-based **warehouse management simulator** with real-time grid visualization, robot pathfinding, and dynamic inventory control.

---

## ✨ Features

- 🗺️ 11x11 **Visual Warehouse Grid** with live updates  
- 🤖 **Robot Agents** with adjustable speed  
- 🧑‍💼 **Employee Management** with different roles  
- 📦 **Product Inventory System** with stock tracking  
- 🔄 **Order Pipeline** (Pending → Processing → Packed)  
- 🧠 **A*** Pathfinding for smart navigation  
- 🎨 **Color-coded Console Output**  
- 💾 **JSON-based Data Storage** for all entities

---

## 🚀 Getting Started

### 📦 Prerequisites

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### 🔧 Installation

```bash
git clone https://github.com/yourusername/warehouse-simulator.git
cd warehouse-simulator
dotnet run
```

---

## 🗂 Project Structure

```
WarehouseSimulator/
├── Data/           # JSON data files
├── Models/         # Core models: Robot, Order, Product, etc.
├── Services/       # Business logic: RobotService, OrderService, etc.
├── Utils/          # Helpers: Pathfinder, Logger, Visualizer
└── Program.cs      # Entry point
```

---

## 🧠 How It Works

### 🏠 Main Menu

```
=== Warehouse Management System ===

1. Orders      → Create & manage orders  
2. Products    → View & edit product inventory  
3. Employees   → Manage warehouse staff  
4. Robots      → Configure robot agents  
5. Exit
```

---

### 🔁 Simulation Flow

- Create orders with selected products  
- Robots calculate paths using A* and pick up items  
- Deliver them to packing station  
- Employees finalize the order  
- All actions displayed live on the grid

---

## 🧱 Warehouse Grid Example

```
=== WAREHOUSE LAYOUT ===
. . . . . . . . . . .
. [I] . . [ ] . . [I] . . .
. . . . . . . . . . .
. [I] . R . [ ] . [ ] . . .
. . . . . . . . . . .
. [ ] . . [I] . . [ ] . . .
. . . . . . . . . . .
. [ ] . . [ ] . . [I] . . .
. . . . . . . . . . .
. [I] . . [ ] . . [ ] . . .
. . . . . . . . . {$}
```

**Legend**  
- `[I]` - Shelf with inventory  
- `[ ]` - Empty shelf  
- `R` - Robot  
- `{$}` - Packing station  
- `.` - Empty space

---

## ⚙️ Configuration

Edit these files inside `/Data`:

| File            | Description                     |
|-----------------|----------------------------------|
| `Products.json` | Inventory items                 |
| `Robots.json`   | Robot start positions & speed   |
| `Employees.json`| Employee setup                  |
| `Orders.json`   | Initial pending orders          |
| `Shelves.json`  | Warehouse layout                |

---

## 🧑‍💻 Technical Details

### 🧭 Pathfinding

- ✅ A* algorithm with Manhattan heuristic  
- 🚧 Avoids shelves (obstacles)  
- 🔁 Reconstructs shortest paths  
- 📐 Checks adjacent tiles for best paths

### ⚙️ Asynchronous Movement

- `async/await` for robot motion  
- ⏱ Configurable movement delay  
- 🖥 Non-blocking console updates

---

## 📄 License

Distributed under the **MIT License**.  
See [`LICENSE`](https://github.com/Noa-Medi/WarehouseSimulator/blob/b6927661b1863fc2c064c244420dfb37793a0a80/LICENSE) file for more info.

---

## 📬 Contact

**Mehdi Abbasi** – Mehdi.abbass1386@gmail.com  
🌐 [Project Repo](https://github.com/Noa-Medi/WarehouseSimulator)
