-- CreateEnum
CREATE TYPE "OrderStatus" AS ENUM ('PENDING', 'SUCCESS', 'FAILED');

-- CreateTable
CREATE TABLE "Purchase" (
    "id" SERIAL NOT NULL,
    "orderId" TEXT NOT NULL,
    "purchaseAmount" INTEGER NOT NULL,
    "orderStatus" "OrderStatus" NOT NULL DEFAULT 'PENDING',

    CONSTRAINT "Purchase_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "Purchase_orderId_key" ON "Purchase"("orderId");
