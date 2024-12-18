
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizePass : ScriptableRenderPass {
	private Shader pixelateShader;
	private int screenHeight;

	private RenderTargetIdentifier colorBuffer, pixelBuffer;
	private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

	private Material material;
	private int pixelScreenHeight, pixelScreenWidth;

	public PixelizePass(Shader pixelateShader, RenderPassEvent renderPassEvent, int screenHeight) {
		this.renderPassEvent = renderPassEvent;
		this.screenHeight = screenHeight;
		if (material == null) material = new Material(pixelateShader);
	}

	public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
		colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
		RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

		pixelScreenHeight = screenHeight;
		pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

		material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
		material.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
		material.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

		descriptor.height = pixelScreenHeight;
		descriptor.width = pixelScreenWidth;

		cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
		pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
	}

	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
		CommandBuffer cmd = CommandBufferPool.Get();
		using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass"))) {
			Blit(cmd, colorBuffer, pixelBuffer, material);
			Blit(cmd, pixelBuffer, colorBuffer);
		}

		context.ExecuteCommandBuffer(cmd);
		CommandBufferPool.Release(cmd);
	}

	public override void OnCameraCleanup(CommandBuffer cmd) {
		if (cmd == null) throw new System.ArgumentNullException("cmd");
		cmd.ReleaseTemporaryRT(pixelBufferID);
		//cmd.ReleaseTemporaryRT(pointBufferID);
	}

}
