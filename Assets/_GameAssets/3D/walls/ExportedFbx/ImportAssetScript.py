def ImportAllAssets():
#This script was generated with the addons Blender for UnrealEngine : https://github.com/xavier150/Blender-For-UnrealEngine-Addons
#This script will import in unreal all camera in target sequencer
#The script must be used in Unreal Engine Editor with UnrealEnginePython : https://github.com/20tab/UnrealEnginePython
#Use this command : unreal_engine.py_exec(r"F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\ImportAssetScript.py")


	import os.path
	import configparser
	import ast
	import unreal_engine as ue
	from unreal_engine.classes import PyFbxFactory, AlembicImportFactory, StaticMesh, Skeleton, SkeletalMeshSocket
	from unreal_engine.enums import EFBXImportType, EMaterialSearchLocation, ECollisionTraceFlag
	from unreal_engine.structs import StaticMeshSourceModel, MeshBuildSettings
	from unreal_engine import FVector, FRotator
	
	
	#Prepare var and def
	unrealImportLocation = r'/Game/ImportedFbx'
	ImportedList = []
	ImportFailList = []
	
	def GetOptionByIniFile(FileLoc, OptionName, literal = False):
		Config = configparser.ConfigParser()
		Config.read(FileLoc)
		Options = []
		for option in Config.options(OptionName):
			if (literal == True):
				Options.append(ast.literal_eval(Config.get(OptionName, option)))
			else:
				Options.append(Config.get(OptionName, option))
		return Options
	
	
	#Process import
	print('========================= Import started ! =========================')
	
	
	
	
	'''
	<##############################################################################>
	<#############################	           		#############################>
	<############################	           		 ############################>
	<############################	 StaticMesh tasks	 ############################>
	<############################	           		 ############################>
	<#############################	           		#############################>
	<##############################################################################>
	'''
	
	StaticMesh_TasksList = []
	StaticMesh_PreImportPath = []
	print('========================= Creating StaticMesh tasks... =========================')
	
	def CreateTask_SM_corner():
		################[ Import corner as StaticMesh type ]################
		print('================[ New import task : corner as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_corner.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_corner_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : corner ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "corner" not found for after inport')
			return
		print('========================= Imports of corner completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of corner completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_corner()
	
	
	
	
	def CreateTask_SM_firstFloor():
		################[ Import firstFloor as StaticMesh type ]################
		print('================[ New import task : firstFloor as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_firstFloor.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_firstFloor_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : firstFloor ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "firstFloor" not found for after inport')
			return
		print('========================= Imports of firstFloor completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of firstFloor completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_firstFloor()
	
	
	
	
	def CreateTask_SM_base():
		################[ Import base as StaticMesh type ]################
		print('================[ New import task : base as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_base.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_base_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : base ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "base" not found for after inport')
			return
		print('========================= Imports of base completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of base completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_base()
	
	
	
	
	def CreateTask_SM_secondFloor():
		################[ Import secondFloor as StaticMesh type ]################
		print('================[ New import task : secondFloor as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_secondFloor.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_secondFloor_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : secondFloor ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "secondFloor" not found for after inport')
			return
		print('========================= Imports of secondFloor completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of secondFloor completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_secondFloor()
	
	
	
	
	def CreateTask_SM_deuxiemeEtage():
		################[ Import deuxiemeEtage as StaticMesh type ]################
		print('================[ New import task : deuxiemeEtage as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_deuxiemeEtage.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_deuxiemeEtage_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : deuxiemeEtage ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "deuxiemeEtage" not found for after inport')
			return
		print('========================= Imports of deuxiemeEtage completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of deuxiemeEtage completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_deuxiemeEtage()
	
	
	
	
	def CreateTask_SM_cornerB():
		################[ Import cornerB as StaticMesh type ]################
		print('================[ New import task : cornerB as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_cornerB.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_cornerB_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : cornerB ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "cornerB" not found for after inport')
			return
		print('========================= Imports of cornerB completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of cornerB completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_cornerB()
	
	
	
	
	def CreateTask_SM_simpleWall():
		################[ Import simpleWall as StaticMesh type ]################
		print('================[ New import task : simpleWall as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_simpleWall.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_simpleWall_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : simpleWall ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "simpleWall" not found for after inport')
			return
		print('========================= Imports of simpleWall completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of simpleWall completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_simpleWall()
	
	
	
	
	def CreateTask_SM_base1():
		################[ Import base1 as StaticMesh type ]################
		print('================[ New import task : base1 as StaticMesh type ]================')
		FilePath = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_base1.fbx')
		AdditionalParameterLoc = os.path.join(r'F:\ENJMIN\unity\enjminProjProg\Assets\_GameAssets\3D\walls\ExportedFbx\StaticMesh\SM_base1_AdditionalParameter.ini')
		AssetImportPath = (os.path.join(unrealImportLocation, r'').replace('\\','/')).rstrip('/')
		task = PyFbxFactory()
		task.ImportUI.MeshTypeToImport = EFBXImportType.FBXIT_StaticMesh
		task.ImportUI.bImportMaterials = True
		task.ImportUI.bImportTextures = False
		task.ImportUI.bImportAnimations = False
		task.ImportUI.bImportMesh = True
		task.ImportUI.bCreatePhysicsAsset = True
		task.ImportUI.TextureImportData.MaterialSearchLocation = EMaterialSearchLocation.Local
		task.ImportUI.StaticMeshImportData.bCombineMeshes = True
		task.ImportUI.StaticMeshImportData.bAutoGenerateCollision = True
		task.ImportUI.StaticMeshImportData.bGenerateLightmapUVs = True
		print('================[ import asset : base1 ]================')
		try:
			asset = task.factory_import_object(FilePath, AssetImportPath)
		except:
			asset = None
		if asset == None:
			ImportFailList.append('Asset "base1" not found for after inport')
			return
		print('========================= Imports of base1 completed ! Post treatment started...	=========================')
		asset.BodySetup.CollisionTraceFlag = ECollisionTraceFlag.CTF_UseDefault 
		lods_to_add = GetOptionByIniFile(AdditionalParameterLoc, 'LevelOfDetail')
		for x, lod in enumerate(lods_to_add):
			pass
			asset.static_mesh_import_lod(lod, x+1)
		print('========================= Post treatment of base1 completed !	 =========================')
		asset.save_package()
		asset.post_edit_change()
		ImportedList.append([asset, 'StaticMesh'])
	CreateTask_SM_base1()
	
	
	
	
	print('========================= Full import completed !  =========================')
	
	StaticMesh_ImportedList = []
	SkeletalMesh_ImportedList = []
	Alembic_ImportedList = []
	Animation_ImportedList = []
	for asset in ImportedList:
		if asset[1] == 'StaticMesh':
			StaticMesh_ImportedList.append(asset[0])
		elif asset[1] == 'SkeletalMesh':
			SkeletalMesh_ImportedList.append(asset[0])
		elif asset[1] == 'Alembic':
			Alembic_ImportedList.append(asset[0])
		else:
			Animation_ImportedList.append(asset[0])
	
	print('Imported StaticMesh: '+str(len(StaticMesh_ImportedList)))
	print('Imported SkeletalMesh: '+str(len(SkeletalMesh_ImportedList)))
	print('Imported Alembic: '+str(len(Alembic_ImportedList)))
	print('Imported Animation: '+str(len(Animation_ImportedList)))
	print('Import failled: '+str(len(ImportFailList)))
	for error in ImportFailList:
		print(error)
	
	print('=========================')
	if len(ImportFailList) == 0:
		return 'Assets imported with success !' 
	else:
		return 'Some asset(s) could not be imported.' 
	
print(ImportAllAssets())
